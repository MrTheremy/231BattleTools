using System.Net.WebSockets;
using System.Text.Json;
using KingshotTools.Contracts.Frames;
using KingshotTools.Ingest;
using KingshotTools.Ingest.Queue;
using Microsoft.Extensions.Options;

namespace KingshotTools.Api.Features.Ingest;

/// <summary>
/// WebSocket endpoint agents upload <see cref="FrameBatch"/> messages to.
/// Stub: accepts JSON batches, logs them and hands them to the ingest queue.
/// </summary>
public static class IngestWebSocketEndpoint
{
    public const string Route = "/ingest/ws";

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public static IEndpointRouteBuilder MapIngestWebSocket(this IEndpointRouteBuilder endpoints)
    {
        endpoints.Map(Route, HandleAsync).ExcludeFromDescription();
        return endpoints;
    }

    private static async Task HandleAsync(
        HttpContext context,
        IIngestQueue queue,
        IOptions<IngestOptions> options,
        ILoggerFactory loggerFactory)
    {
        if (!context.WebSockets.IsWebSocketRequest)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }

        // TODO: authenticate the agent before accepting.
        var logger = loggerFactory.CreateLogger(typeof(IngestWebSocketEndpoint).FullName!);
        using var socket = await context.WebSockets.AcceptWebSocketAsync();
        var cancellationToken = context.RequestAborted;
        var maxBytes = options.Value.MaxMessageBytes;
        var buffer = new byte[16 * 1024];

        logger.LogInformation("Agent connected to ingest from {RemoteIp}", context.Connection.RemoteIpAddress);

        while (socket.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
        {
            using var message = new MemoryStream();
            WebSocketReceiveResult result;
            do
            {
                result = await socket.ReceiveAsync(buffer, cancellationToken);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, cancellationToken);
                    logger.LogInformation("Agent closed the ingest connection");
                    return;
                }

                message.Write(buffer, 0, result.Count);
                if (message.Length > maxBytes)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.MessageTooBig, "Batch too large", cancellationToken);
                    logger.LogWarning("Closed ingest connection: message exceeded {MaxMessageBytes} bytes", maxBytes);
                    return;
                }
            }
            while (!result.EndOfMessage);

            FrameBatch? batch;
            try
            {
                // TODO: switch to a compact binary encoding once the agent uploader exists.
                batch = JsonSerializer.Deserialize<FrameBatch>(message.GetBuffer().AsSpan(0, (int)message.Length), JsonOptions);
            }
            catch (JsonException ex)
            {
                logger.LogWarning(ex, "Discarded malformed ingest message of {Bytes} bytes", message.Length);
                continue;
            }

            if (batch is null)
            {
                continue;
            }

            logger.LogInformation(
                "Received batch {SequenceNumber} from agent {AgentId} session {SessionId} with {FrameCount} frames",
                batch.SequenceNumber,
                batch.AgentId,
                batch.SessionId,
                batch.Frames.Count);

            await queue.EnqueueAsync(batch, cancellationToken);

            // TODO: send an ack {sequenceNumber} so the agent can trim its spool.
        }
    }
}

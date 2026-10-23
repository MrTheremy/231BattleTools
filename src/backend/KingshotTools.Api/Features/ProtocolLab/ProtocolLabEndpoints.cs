namespace KingshotTools.Api.Features.ProtocolLab;

/// <summary>Admin area for investigating message ids the decoder does not know yet.</summary>
public static class ProtocolLabEndpoints
{
    public static IEndpointRouteBuilder MapProtocolLab(this IEndpointRouteBuilder endpoints)
    {
        // TODO: RequireAuthorization("admin") once auth exists.
        var group = endpoints.MapGroup("/api/admin/protocol-lab").WithTags("Protocol Lab");

        group.MapGet("/unknown-messages", () => TypedResults.Ok(Array.Empty<UnknownMessageSummary>()))
            .WithName("ListUnknownMessages")
            .WithSummary("Message ids seen on the wire that are missing from the schema bundle.");

        return endpoints;
    }
}

/// <param name="MessageId">Protocol message id.</param>
/// <param name="Count">Times seen.</param>
/// <param name="LastSeen">Most recent sighting.</param>
public sealed record UnknownMessageSummary(int MessageId, long Count, DateTimeOffset LastSeen);

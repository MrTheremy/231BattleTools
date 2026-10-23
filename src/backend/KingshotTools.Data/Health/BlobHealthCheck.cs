using Azure;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace KingshotTools.Data.Health;

public sealed class BlobHealthCheck(BlobServiceClient client) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await client.GetPropertiesAsync(cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex) when (ex is RequestFailedException or AggregateException)
        {
            return HealthCheckResult.Unhealthy("Blob Storage is unreachable.", ex);
        }
    }
}

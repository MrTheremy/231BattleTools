using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace KingshotTools.Data.Health;

public sealed class CosmosHealthCheck(CosmosClient client) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await client.ReadAccountAsync();
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex) when (ex is CosmosException or HttpRequestException or TaskCanceledException)
        {
            return HealthCheckResult.Unhealthy("Cosmos DB is unreachable.", ex);
        }
    }
}

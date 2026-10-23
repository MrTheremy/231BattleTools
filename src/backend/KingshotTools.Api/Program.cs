using KingshotTools.Api.Features.Ingest;
using KingshotTools.Api.Features.ProtocolLab;
using KingshotTools.Data;
using KingshotTools.Data.Bootstrap;
using KingshotTools.Ingest;
using KingshotTools.Live;
using KingshotTools.Live.Hubs;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog((services, logger) => logger
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());

builder.Services.AddOpenApi();
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddKingshotData(builder.Configuration);

// Must be registered before the hosted services that need the containers (ingest worker, change feed).
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddHostedService<StorageBootstrapHostedService>();
}

builder.Services.AddKingshotIngest(builder.Configuration);
builder.Services.AddKingshotLive(builder.Configuration);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy
    .WithOrigins(builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [])
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()));

// TODO: authentication/authorization (agent tokens for ingest, user login for the web app, admin role for the Protocol Lab).

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseCors();
app.UseWebSockets();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapHealthChecks("/health");
app.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false });

app.MapIngestWebSocket();
app.MapProtocolLab();
app.MapHub<LiveHub>(LiveHub.Route);

app.Run();

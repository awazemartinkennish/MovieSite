using MovieSite.Database;
using MovieSite.MigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<MovieDbContext>("Movies");

var host = builder.Build();
host.Run();

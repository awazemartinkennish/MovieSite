var builder = DistributedApplication.CreateBuilder(args);

// PostgreSQL container is configured with an auto-generated password by default
// and supports setting the default database name via an environment variable & running *.sql/*.sh scripts in a bind mount.
string moviesDbName = "Movies";
var movieDb = builder.AddPostgres("postgres")
    // Set the name of the default database to auto-create on container startup.                                
    .WithEnvironment("POSTGRES_DB", moviesDbName)
    // Mount the SQL scripts directory into the container so that the init scripts run.
    .WithBindMount("../MovieSite.ApiService/data/postgres", "/docker-entrypoint-initdb.d")
    // Add the default database to the application model so that it can be referenced by other resources.
    .AddDatabase(moviesDbName);

var apiService = builder.AddProject<Projects.MovieSite_ApiService>("apiservice")
    .WithReference(movieDb);

builder.AddProject<Projects.MovieSite_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.AddProject<Projects.MovieSite_MigrationService>("migrations")
       .WithReference(movieDb);

builder.Build().Run();

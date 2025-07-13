using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder.AddKeycloak("keycloak", 8080)
    .WithDataVolume();

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin();

var db = postgres.AddDatabase("sandbox-db");

var migrations = builder.AddProject<Projects.Sandbox_Migrations>("sandbox-migrations")
    .WithReference(db)
    .WaitFor(postgres)
    .WaitFor(db);

if(builder.Environment.IsDevelopment())
{
    migrations.WithHttpCommand(path: "/reset-db", displayName: "Reset Database", commandOptions: new HttpCommandOptions
    {
        IconName = "fa-solid fa-database",
    });
}

builder.AddProject<Projects.Sandbox_API>("sandbox-api");

builder.Build().Run();

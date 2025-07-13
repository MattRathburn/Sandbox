var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Sandbox_API>("sandbox-api");

builder.Build().Run();

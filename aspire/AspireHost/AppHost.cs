using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<BffService>("BffService");
builder.AddProject<AnkiCommunicationService>("AnkiCommunicationService");
builder.AddProject<DesktopService>("DesktopService");

builder.Build().Run();

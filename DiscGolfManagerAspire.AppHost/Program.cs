var builder = DistributedApplication.CreateBuilder(args);

var discs = builder.AddProject<Projects.Discs>("discs");

var apiService = builder.AddProject<Projects.DiscGolfManagerAspire_ApiService>("apiservice")
    .WithReference(discs);

builder.AddProject<Projects.DiscGolfManagerAspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);



builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);

var discs = builder.AddProject<Projects.Discs>("discs");

var apiService = builder.AddProject<Projects.DiscGolfManagerAspire_DiscsApiService>("discsapiservice");
    //.WithReference(discs);

builder.AddProject<Projects.DiscGolfManagerAspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);



builder.Build().Run();

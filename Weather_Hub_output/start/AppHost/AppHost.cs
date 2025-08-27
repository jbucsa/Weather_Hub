var builder = DistributedApplication.CreateBuilder(args);

var myCache = builder.AddRedis("cache");

var myAPI = builder.AddProject<Projects.Api>("api");

var web = builder.AddProject<Projects.MyWeatherHub>("myweatherhub")
    .WithReference(myAPI)
    .WithExternalHttpEndpoints();



builder.Build().Run();

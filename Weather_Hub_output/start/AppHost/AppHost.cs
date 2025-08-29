var builder = DistributedApplication.CreateBuilder(args);

// 'myCache is a varible name; while 'cache' is a serve name
// Additionally we can add customizable Redis container images from Docker Host.
var myCache = builder.AddRedis("cache")
    // Uncomment line to connect 'Garnet' image as Redis Cache tool
    // .WithImageRegistry("ghcr.io")
    // .WithImage("microsoft/garnet")
    // .WithImageTag("latest");

    // Comment out to connect 'Garnet' image as Redis Cache tool and turn off Redis Commander tool.
    .WithRedisCommander();

var myAPI = builder.AddProject<Projects.Api>("api")
    .WithReference(myCache);

var web = builder.AddProject<Projects.MyWeatherHub>("myweatherhub")
    .WithReference(myAPI)
    .WithExternalHttpEndpoints();

builder.Build().Run();

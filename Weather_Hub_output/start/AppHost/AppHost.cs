var builder = DistributedApplication.CreateBuilder(args);

// 'myCache is a varible name; while 'cache' is a serve name
var myCache = builder.AddRedis("cache").WithRedisCommander();

var myAPI = builder.AddProject<Projects.Api>("api").WithReference(myCache);

var web = builder.AddProject<Projects.MyWeatherHub>("myweatherhub")
    .WithReference(myAPI)
    .WithExternalHttpEndpoints();



builder.Build().Run();

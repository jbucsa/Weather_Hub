using MyWeatherHub;
using MyWeatherHub.Components;
using Microsoft.AspNetCore.OutputCaching;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
// builder.Services.AddSwaggerGen(); // Removed for now
builder.Services.AddStackExchangeRedisOutputCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("cache");
});

builder.AddServiceDefaults();

builder.Services.AddHttpClient<NwsManager>(c =>
{
    var url = builder.Configuration["WeatherEndpoint"] ?? throw new InvalidOperationException("WeatherEndpoint is not set");
    c.BaseAddress = new(url);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
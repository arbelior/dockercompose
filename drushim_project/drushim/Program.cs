using drushim.Appsettings;
using drushim.BHelpers;
using drushim.BL;
using drushim.Logger;
using drushim.Models.Proxy;
using drushim.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // מאזין על פורט 80 בדוקר
});

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("cities.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var citiesMap = builder.Configuration.GetSection("Cities").Get<Dictionary<string, string>>();
builder.Services.AddSingleton(citiesMap);

builder.Services.AddScoped<ISearchCityServer, SearchCityServer>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

//var cacheDuration = builder.Configuration.GetValue<int>("CachSettings:OutputCacheDurationSeconds");

//builder.Services.AddControllers(options =>
//{
//    options.CacheProfiles.Add("Cachedefault", new CacheProfile
//    {
//        Duration = cacheDuration
//    });
//});

//builder.Services.AddControllers();

//builder.Services.AddHttpClient();




//var proxySettings = builder.Configuration.GetSection("ProxySettings");

//    builder.Services.AddHttpClient("WithProxy")
//    .ConfigurePrimaryHttpMessageHandler(() =>
//    {
//        var proxy = new WebProxy($"{proxySettings["Address"]}:{proxySettings["Port"]}")
//        {
//            BypassProxyOnLocal = bool.Parse(proxySettings["BypassOnLocal"] ?? "false"),
//            UseDefaultCredentials = true // ��� ����� ���
//        };

//        return new HttpClientHandler
//        {
//            Proxy = proxy,
//            UseProxy = true,
//            UseDefaultCredentials = bool.Parse(proxySettings["UseDefaultWebProxy"] ?? "false")
//        };
//    });


//var proxySettings = builder.Configuration.GetSection("ProxySettings").Get<ProxySettings>();

//builder.Services.AddHttpClient("WithProxy")
//    .ConfigurePrimaryHttpMessageHandler(() =>
//    {
//        var proxy = new WebProxy($"{proxySettings.Address}:{proxySettings.Port}")
//        {
//            BypassProxyOnLocal = proxySettings.BypassOnLocal,
//            UseDefaultCredentials = true
//        };

//        return new HttpClientHandler
//        {
//            Proxy = proxy,
//            UseProxy = true,
//            UseDefaultCredentials = proxySettings.UseDefaultWebProxy
//        };
//    });


//builder.Services.AddHttpClient("WithProxy")
//    .ConfigurePrimaryHttpMessageHandler(() =>
//    {
//        var proxy = new WebProxy($"{proxySettings.Address}:{proxySettings.Port}")
//        {
//            BypassProxyOnLocal = proxySettings.BypassOnLocal,
//            UseDefaultCredentials = true
//        };

//        return new HttpClientHandler
//        {
//            Proxy = proxy,
//            UseProxy = true,
//            UseDefaultCredentials = proxySettings.UseDefaultWebProxy,
//            PreAuthenticate = true,
//            UseCookies = true,
//            AllowAutoRedirect = true,
//            MaxConnectionsPerServer = int.MaxValue
//        };
//    });


//builder.Services.AddHttpClient("WithProxy", client =>
//{
//    client.Timeout = TimeSpan.FromSeconds(30); // ?? ���� Timeout ����
//    client.DefaultRequestHeaders.ExpectContinue = false; // ?? ��� ExpectContinue �-False
//})
//.ConfigurePrimaryHttpMessageHandler(() =>
//{
//    var proxy = new WebProxy($"{proxySettings.Address}:{proxySettings.Port}")
//    {
//        BypassProxyOnLocal = proxySettings.BypassOnLocal,
//        UseDefaultCredentials = true
//    };

//    return new HttpClientHandler
//    {
//        Proxy = proxy,
//        UseProxy = true,
//        UseDefaultCredentials = proxySettings.UseDefaultWebProxy,
//        PreAuthenticate = true,
//        AllowAutoRedirect = true,
//        UseCookies = true,
//        MaxConnectionsPerServer = int.MaxValue,
//        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator, // ?? �� �� ����� ����� SSL (����� ����)
//    };
//})
//.AddTransientHttpErrorPolicy(policyBuilder =>
//    policyBuilder.WaitAndRetryAsync(
//        retryCount: 3,
//        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
//    )
//);


var cacheDuration = builder.Configuration.GetValue<int>("CachSettings:OutputCacheDurationSeconds");

builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("Cachedefault", new CacheProfile
    {
        Duration = cacheDuration
    });
});

//builder.Services.AddControllers(options =>
//{
//    options.CacheProfiles.Add("Cachedefault", new CacheProfile
//    {
//        Duration = cacheDuration
//    });
//});

//builder.Services.AddControllers();




builder.Services.AddSingleton<Ilogger, logger>();

builder.Services.AddScoped<ServiceFacade>();

builder.Services.AddHttpClient("WithProxy");




//builder.Services.AddScoped<BertService>();

//builder.Services.AddScoped<TokenizerHelper>();

builder.Services.AddScoped<AppSettingsConfig>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

bool EnableWriteLog = builder.Configuration.GetValue<bool>("StartWritetologfile");

if (EnableWriteLog)
{
    string environment = builder.Environment.EnvironmentName;
    string logPath = BLHelper.Createfiledirectorey();
    await File.AppendAllTextAsync(logPath, $"{DateTime.Now}: Current Environment: {environment}\n");
}

if (MachineMode.IsDebug())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

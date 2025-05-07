
using SimiltaryWorlds.Facades;
using SimiltaryWorlds.Helpers;
using SimiltaryWorlds.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using OpenAI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("cities.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("categories.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("RelevantJobs.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

bool IsAiMotorActive = builder.Configuration.GetValue<bool>("AiSearchMotor");

var Categorey_Map = builder.Configuration.Get<Dictionary<string, List<int>>>() ?? new Dictionary<string, List<int>>();

var cityMap = builder.Configuration.Get<Dictionary<string, string>>() ?? new Dictionary<string, string>();

var RelevantJobs = builder.Configuration.Get<List<string>>() ?? new List<string>();

var openAiApiKey = builder.Configuration.GetSection("OpenAI:ApiKey").Value ?? "";

//var Categorey_Map = builder.Configuration.GetSection("CategoryMap").Get<Dictionary<string, List<int>>>()
//                    ?? new Dictionary<string, List<int>>();

//var cityMap = builder.Configuration.GetSection("CityMap").Get<Dictionary<string, string>>()
//              ?? new Dictionary<string, string>();

//var RelevantJobs = builder.Configuration.GetSection("RelevantJobs").Get<List<string>>()
//                   ?? new List<string>();

var GetAllJobs = builder.Configuration.Get<List<string>>() ?? new List<string>();

builder.Services.AddSingleton(GetAllJobs);

builder.Services.AddSingleton(Categorey_Map);
builder.Services.AddSingleton(RelevantJobs);

builder.Services.AddSingleton(openAiApiKey);


// הוסף את המילון ואת CityHelper לשירותים
builder.Services.AddSingleton(cityMap);
builder.Services.AddSingleton<CityHelper>();

// הוסף את שאר השירותים
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddScoped<EmbeddingFacade>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<ISearchCityServer, SearchCityServer>();
builder.Services.AddScoped<Ifacadelogic, facadelogic>();
builder.Services.AddScoped<IfreeSearch, FreeSearch>();

builder.Services.AddOpenAIService(settings =>
{
    settings.ApiKey = openAiApiKey;
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// צור את קובץ המילים עם embedding רק בסביבת פיתוח
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var jobService = scope.ServiceProvider.GetRequiredService<IJobService>();
    await jobService.SaveUniqueWordEmbeddingsToFileAsync("word_embeddings.json");

    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


using BSocialMedia_Grabber.Services;
using dotenv.net;
using Scalar.AspNetCore;
using Microsoft.OpenApi.Models;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");

if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
{
    throw new InvalidOperationException("Las variables de entorno SUPABASE_URL y SUPABASE_KEY deben estar configuradas.");
}

// Registrar el servicio de Supabase
builder.Services.AddSingleton<SupabaseService>(provider =>
{
    var service = new SupabaseService(url, key);
    service.InitializeAsync().GetAwaiter().GetResult(); // Inicializar sincrónicamente
    return service;
});

builder.Services.AddHttpClient<ScraperService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(60);
}); 

builder.Services.AddScoped<ScraperService>();

builder.Services.AddControllers();
// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BSocialMedia API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BSocialMedia API v1");
    });
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
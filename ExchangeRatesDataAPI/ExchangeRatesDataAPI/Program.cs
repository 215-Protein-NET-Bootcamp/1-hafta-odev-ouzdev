using CurrencyConverterAPI.Adapters.ExchangeRatesService;
using FluentValidation.AspNetCore;
using System.Reflection;
using Serilog;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
                .AddFluentValidation(c =>
                c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.File(@"logs\log.txt", rollingInterval: RollingInterval.Day));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Exchange Rates Data API",
        Version = "v1",
        Description = "15 ve �zeri veri kayna��ndan anl�k olarak 170 para biriminin kur de�erlerini getiren API projesidir." +
        "Bu API, girilen miktar�, se�ilen iki kura g�re de�erini hesaplayan, se�ilen para birimine g�re di�er para birimlerinin de�erlerini getiren ve desteklenen t�m kurlar� listeleyen 3 tane endpointe sahiptir.",
        Contact = new OpenApiContact
        {
            Name = "O�uzcan Gen�",
            Email = "oguzcangencc@hotmail.com",
            Url = new Uri("https://oguzcangenc.com"),
        }
    });
});
builder.Services.AddHttpClient("ExchangeRateData", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["CurrencyConverterService:APIURL"]);
    httpClient.DefaultRequestHeaders.Add("apikey", builder.Configuration["CurrencyConverterService:APIKEY"]);
});

builder.Services.AddScoped<IExchangeRateService, ExchangeRateManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using CurrencyConverterAPI.Adapters.ExchangeRatesService;
using CurrencyConverterAPI.Models;
using CurrencyConverterAPI.ValidationRules.FluentValidation;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
                .AddFluentValidation(c =>
                c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.File(@"logs\log.txt", rollingInterval: RollingInterval.Day));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
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
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

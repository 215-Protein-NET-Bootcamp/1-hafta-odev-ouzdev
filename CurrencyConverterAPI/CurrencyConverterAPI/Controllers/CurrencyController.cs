using CurrencyConverterAPI.Adapters.ExchangeRatesService;
using CurrencyConverterAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IExchangeRateService exchangeRateService;

        public CurrencyController(IExchangeRateService exchangeRateService)
        {
            this.exchangeRateService = exchangeRateService;
        }

        [HttpPost]
        public async Task<ExchangeRateResponse> ConvertCurrency([FromQuery]ConvertCurrencyRate currencyRate)
        {
            var response = await exchangeRateService.ConvertCurrency(currencyRate);
            return response;
        }


        [HttpPost("LatestCurrency")]
        public async Task<ExchangeLatestResponse> LatestCurrency([FromQuery] string baseCurrency,string? symbols)
        {
            var response = await exchangeRateService.GetLatestCurrency(baseCurrency, symbols);
            return response;
        }
        [HttpGet("SupporrtedCurrencies")]
        public async Task<IDictionary<string, string>> SupportedCurrencies()
        {
            var response = await exchangeRateService.GetSupportedCurrencies();
            return response;
        }
    }
}

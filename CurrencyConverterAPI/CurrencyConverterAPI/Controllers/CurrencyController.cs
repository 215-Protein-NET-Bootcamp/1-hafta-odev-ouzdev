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
        public async Task<IActionResult> ConvertCurrency([FromQuery]ConvertCurrencyRate currencyRate)
        {
            var response = await exchangeRateService.ConvertCurrency(currencyRate);

            return response.Success ? Ok(response) : BadRequest(response);
        }


        [HttpPost("LatestCurrency")]
        public async Task<IActionResult> LatestCurrency([FromQuery] string baseCurrency,string? symbols)
        {
            var response = await exchangeRateService.GetLatestCurrency(baseCurrency, symbols);

            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpGet("SupporrtedCurrencies")]
        public async Task<IActionResult> SupportedCurrencies()
        {
            var response = await exchangeRateService.GetSupportedCurrencies();

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}

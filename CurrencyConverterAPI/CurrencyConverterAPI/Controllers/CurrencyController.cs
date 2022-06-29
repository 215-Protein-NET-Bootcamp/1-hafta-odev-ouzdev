using CurrencyConverterAPI.Adapters.ExchangeRatesService;
using CurrencyConverterAPI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IExchangeRateService exchangeRateService;

        public CurrencyController(IExchangeRateService exchangeRateService, IValidator<ConvertCurrencyRate> validator)
        {
            this.exchangeRateService = exchangeRateService;
        }

        [HttpGet("ConvertCurrency")]
        public async Task<IActionResult> ConvertCurrency([FromQuery]ConvertCurrencyRate currencyRate)
        {
            var response = await exchangeRateService.ConvertCurrency(currencyRate);

            return response.Success ? Ok(response) : BadRequest(response);
           
        }


        [HttpGet("LatestCurrency")]
        public async Task<IActionResult> LatestCurrency([FromQuery] LatestCurrencyRate latestCurrencyRate)
        {
            var response = await exchangeRateService.GetLatestCurrency(latestCurrencyRate);

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

using CurrencyConverterAPI.Adapters.ExchangeRatesService.Abstract;
using CurrencyConverterAPI.Models.Dtos.ConvertCurrencyDtos;
using CurrencyConverterAPI.Models.Dtos.SupportedCurrencyDtos;
using Microsoft.AspNetCore.Http;
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
        public async Task<ExchangeRateResponse> ConvertCurrency([FromQuery]ConvertQueryDto dto)
        {

            var response = await exchangeRateService.ConvertCurrency(dto);
            return response;
        }
        [HttpPost("LatestCurrency")]
        public async Task<ExchangeLatestResponse> LatestCurrency([FromQuery] string baseCurrency)
        {
            var response = await exchangeRateService.GetLatestCurrency(baseCurrency);
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

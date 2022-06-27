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
        public async Task<ExchangeLatestResponse> LatestCurrency([FromQuery] string baseCurrency,List<string> targetCurrencies)
        {
            var response = await exchangeRateService.LatestCurrency();

            return response;
        }
        [HttpGet("SupporrtedCurrencies")]
        public async Task<SupportedCurrencyResponse> SupportedCurrencies()
        {
            var response = await exchangeRateService.SupportedCurrencies();
            return response;
        }
    }
}

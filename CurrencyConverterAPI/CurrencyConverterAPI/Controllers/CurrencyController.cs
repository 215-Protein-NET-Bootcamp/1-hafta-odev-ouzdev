using CurrencyConverterAPI.Adapters.ExchangeRatesService;
using CurrencyConverterAPI.Models;
using CurrencyConverterAPI.Utilities.Result;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

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
       
        [HttpGet("ConvertCurrency")]
        [ProducesResponseType(typeof(SuccessDataResult<ExchangeRateResponse>), 200)]
        public async Task<IActionResult> ConvertCurrency([FromQuery] GetConvertCurrencyQueryObject currencyRate)
        {
            var response = await exchangeRateService.ConvertCurrency(currencyRate);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("LatestCurrency")]
        [ProducesResponseType(typeof(SuccessDataResult<ExchangeLatestResponse>), 200)]
        public async Task<IActionResult> LatestCurrency([FromQuery] GetLatestCurrencyQueryObject latestCurrencyRate)
        {
            var response = await exchangeRateService.GetLatestCurrency(latestCurrencyRate);

            return response.Success ? Ok(response) : BadRequest(response);

        }
        [HttpGet("SupportedCurrencies")]
        [ProducesResponseType(typeof(SuccessDataResult<IDictionary<string, string>>), 200)]
        public async Task<DataResult<IDictionary<string,string>>> SupportedCurrencies()
        {c
            var response = await exchangeRateService.GetSupportedCurrencies();

            return response.Success ? new SuccessDataResult<IDictionary<string, string>>(response.Data, response.Message) : new ErrorDataResult<IDictionary<string, string>>(response.Message); 
        }
    }
}

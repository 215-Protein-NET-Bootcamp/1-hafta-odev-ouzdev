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

        /// <summary>
        /// Girilen değere ve seçilen kurlara göre dönüşüm yapar.
        /// </summary>
        [HttpGet("ConvertCurrency")]
        [ProducesResponseType(typeof(ExchangeRateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> ConvertCurrency([FromQuery] GetConvertCurrencyRequest currencyRate)
        {
            var response = await exchangeRateService.ConvertCurrency(currencyRate);

            return response.Success ? Ok(response) : BadRequest(response);
        }
        /// <summary>
        /// Girilen kura göre diğer kur veya kurların karşılık değerlerini listeler. 
        /// </summary>
        [HttpGet("LatestCurrency")]
        [ProducesResponseType(typeof(SuccessDataResult<ExchangeLatestResponse>), 200)]
        public async Task<IActionResult> LatestCurrency([FromQuery] GetLatestCurrencyRequest latestCurrencyRate)
        {
            var response = await exchangeRateService.GetLatestCurrency(latestCurrencyRate);

            return response.Success ? Ok(response) : BadRequest(response);

        }
        /// <summary>
        /// Desteklenen kurları listeler.
        /// </summary>
        [HttpGet("SupportedCurrencies")]
        [ProducesResponseType(typeof(SuccessDataResult<IDictionary<string, string>>), 200)]
        public async Task<IActionResult> SupportedCurrencies()
        {
            var response = await exchangeRateService.GetSupportedCurrencies();
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}

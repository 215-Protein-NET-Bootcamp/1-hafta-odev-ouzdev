using CurrencyConverterAPI.Models.Dtos.ConvertCurrencyDtos;
using CurrencyConverterAPI.Models.Dtos.SupportedCurrencyDtos;

namespace CurrencyConverterAPI.Adapters.ExchangeRatesService.Abstract
{
    public interface IExchangeRateService
    {
        Task<ExchangeRateResponse> ConvertCurrency(ConvertQueryDto query);
        Task<ExchangeLatestResponse> LatestCurrency();
        Task<SupportedCurrencyResponse> SupportedCurrencies();
    }
}

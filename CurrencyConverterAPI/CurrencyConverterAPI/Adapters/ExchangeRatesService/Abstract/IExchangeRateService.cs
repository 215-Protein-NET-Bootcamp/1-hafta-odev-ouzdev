using CurrencyConverterAPI.Models.Dtos.ConvertCurrencyDtos;
using CurrencyConverterAPI.Models.Dtos.SupportedCurrencyDtos;

namespace CurrencyConverterAPI.Adapters.ExchangeRatesService.Abstract
{
    public interface IExchangeRateService
    {
        Task<ExchangeRateResponse> ConvertCurrency(ConvertQueryDto query);
        Task<ExchangeLatestResponse> GetLatestCurrency(string baseCurrancy);
        Task<IDictionary<string, string>> GetSupportedCurrencies();
    }
}

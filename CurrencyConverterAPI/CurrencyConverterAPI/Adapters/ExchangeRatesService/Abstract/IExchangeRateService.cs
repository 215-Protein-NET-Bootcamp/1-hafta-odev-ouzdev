using CurrencyConverterAPI.Models.Dtos.ConvertCurrencyDtos;

namespace CurrencyConverterAPI.Adapters.ExchangeRatesService.Abstract
{
    public interface IExchangeRateService
    {
        Task<ExchangeRateResponse> ConvertCurrency(ConvertCurrencyRate currencyRate);
        Task<ExchangeLatestResponse> GetLatestCurrency(string baseCurrancy,params string[] symbols);
        Task<IDictionary<string, string>> GetSupportedCurrencies();
    }
}

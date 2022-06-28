using CurrencyConverterAPI.Models;

namespace CurrencyConverterAPI.Adapters.ExchangeRatesService
{
    public interface IExchangeRateService
    {
        Task<ExchangeRateResponse> ConvertCurrency(ConvertCurrencyRate currencyRate);
        Task<ExchangeLatestResponse> GetLatestCurrency(string baseCurrancy, string symbols);
        Task<IDictionary<string, string>> GetSupportedCurrencies();
    }
}

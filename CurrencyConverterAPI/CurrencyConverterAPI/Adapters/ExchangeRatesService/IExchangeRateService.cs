using CurrencyConverterAPI.Models;
using CurrencyConverterAPI.Utilities.Result;

namespace CurrencyConverterAPI.Adapters.ExchangeRatesService
{
    public interface IExchangeRateService
    {
        Task<IDataResult<ExchangeRateResponse>> ConvertCurrency(ConvertCurrencyRate currencyRate);
        Task<IDataResult<ExchangeLatestResponse>> GetLatestCurrency(LatestCurrencyRate latestCurrencyRate);
        Task<IDataResult<IDictionary<string, string>>> GetSupportedCurrencies();
    }
}

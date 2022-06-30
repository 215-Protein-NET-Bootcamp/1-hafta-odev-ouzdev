using CurrencyConverterAPI.Models;
using CurrencyConverterAPI.Utilities.Result;

namespace CurrencyConverterAPI.Adapters.ExchangeRatesService
{
    public interface IExchangeRateService
    {
        Task<IDataResult<ExchangeRateResponse>> ConvertCurrency(GetConvertCurrencyQueryObject currencyRate);
        Task<IDataResult<ExchangeLatestResponse>> GetLatestCurrency(GetLatestCurrencyQueryObject latestCurrencyRate);
        Task<IDataResult<IDictionary<string, string>>> GetSupportedCurrencies();
    }
}

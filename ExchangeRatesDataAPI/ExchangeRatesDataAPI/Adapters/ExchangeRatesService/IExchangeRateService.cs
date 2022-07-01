using CurrencyConverterAPI.Models;
using CurrencyConverterAPI.Utilities.Result;

namespace CurrencyConverterAPI.Adapters.ExchangeRatesService
{
    public interface IExchangeRateService
    {
        Task<IDataResult<ExchangeRateResponse>> ConvertCurrency(GetConvertCurrencyRequest currencyRate);
        Task<IDataResult<ExchangeLatestResponse>> GetLatestCurrency(GetLatestCurrencyRequest latestCurrencyRate);
        Task<IDataResult<IDictionary<string, string>>> GetSupportedCurrencies();
    }
}

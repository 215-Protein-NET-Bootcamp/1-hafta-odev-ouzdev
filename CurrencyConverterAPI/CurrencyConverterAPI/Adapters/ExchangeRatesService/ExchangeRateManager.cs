using CurrencyConverterAPI.Models;
using System.Text.Json;

namespace CurrencyConverterAPI.Adapters.ExchangeRatesService
{
    public class ExchangeRateManager : IExchangeRateService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ExchangeRateManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ExchangeRateResponse> ConvertCurrency(ConvertCurrencyRate currencyRate)
        {

            var httpClient = _httpClientFactory.CreateClient("ExchangeRateData");
            var httpResponseMessage = await httpClient.GetAsync($"convert?to={currencyRate.To}&from={currencyRate.From}&amount={currencyRate.Amount}");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var parseObject = JsonDocument.Parse(contentStream);
                var res = parseObject.RootElement.GetProperty("result").Deserialize<double>();

                return new ExchangeRateResponse
                {
                    From = currencyRate.From,
                    To = currencyRate.To,
                    CalculatedRate = res


                };

            }
            else
            {
                return new ExchangeRateResponse();
            }


        }
        public async Task<ExchangeLatestResponse> GetLatestCurrency(string baseCurrency, string symbols)
        {
            var httpClient = _httpClientFactory.CreateClient("ExchangeRateData");
            var httpResponseMessage = await httpClient.GetAsync($"latest?symbols={symbols}&base={baseCurrency}");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var parseObject = JsonDocument.Parse(contentStream);
                var baseCurrencyResponse = parseObject.RootElement.GetProperty("base").Deserialize<string>();
                var ratesResponse = parseObject.RootElement.GetProperty("rates").Deserialize<IDictionary<string, double>>();
                return new ExchangeLatestResponse
                {
                    Base = baseCurrencyResponse,
                    Rates = ratesResponse
                };

            }
            else
            {
                return new ExchangeLatestResponse();
            }
        }

        public async Task<IDictionary<string, string>> GetSupportedCurrencies()
        {
            var httpClient = _httpClientFactory.CreateClient("ExchangeRateData");
            var httpResponseMessage = await httpClient.GetAsync($"symbols");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                var parseObject = JsonDocument.Parse(contentStream);
                var symbols = parseObject.RootElement.GetProperty("symbols").Deserialize<IDictionary<string, string>>();
                return symbols;
            }
            return null;
        }
    }
}

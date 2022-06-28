using CurrencyConverterAPI.Adapters.ExchangeRatesService.Abstract;
using CurrencyConverterAPI.Models.Dtos.ConvertCurrencyDtos;
using System.Text.Json;

namespace CurrencyConverterAPI.Adapters.ExchangeRatesService.Concrate
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
                var res = JsonSerializer.Deserialize<double>(parseObject.RootElement.GetProperty("result"));

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
        public async Task<ExchangeLatestResponse> GetLatestCurrency(string baseCurrency,string symbols)
        {
            var httpClient = _httpClientFactory.CreateClient("ExchangeRateData");
            var httpResponseMessage = await httpClient.GetAsync($"latest?symbols={symbols}&base={baseCurrency}");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var parseObject = JsonDocument.Parse(contentStream);
                var baseCurrencyResponse = JsonSerializer.Deserialize<string>(parseObject.RootElement.GetProperty("base"));
                var ratesResponse = JsonSerializer.Deserialize<IDictionary<string, double>>(parseObject.RootElement.GetProperty("rates"));
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
                var symbols = JsonSerializer.Deserialize<IDictionary<string,string>>(parseObject.RootElement.GetProperty("symbols"));
                return symbols;
            }
            return null;
        }
    }
}

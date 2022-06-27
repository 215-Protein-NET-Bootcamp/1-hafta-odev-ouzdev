using CurrencyConverterAPI.Adapters.ExchangeRatesService.Abstract;
using CurrencyConverterAPI.Models.Dtos.ConvertCurrencyDtos;
using CurrencyConverterAPI.Models.Dtos.SupportedCurrencyDtos;
using Microsoft.Net.Http.Headers;
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
        public async Task<ExchangeRateResponse> ConvertCurrency(ConvertQueryDto query)
        {

            var httpClient = _httpClientFactory.CreateClient("ExchangeRateData");
            var httpResponseMessage = await httpClient.GetAsync($"convert?to={query.To}&from={query.From}&amount={query.Amount}");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream =
                   await httpResponseMessage.Content.ReadAsStreamAsync();

                var response = await JsonSerializer.DeserializeAsync<ExchangeRateResponse>(contentStream);
                if (response != null)
                {
                    return response;

                }
                return new ExchangeRateResponse();

            }
            else
            {
                return new ExchangeRateResponse();
            }


        }
        public async Task<ExchangeLatestResponse> GetLatestCurrency(string baseCurrency)
        {
            var httpClient = _httpClientFactory.CreateClient("ExchangeRateData");
            var httpResponseMessage = await httpClient.GetAsync($"latest?symbols=&base={baseCurrency}");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream =
                   await httpResponseMessage.Content.ReadAsStreamAsync();
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
                #pragma warning disable CS8603 // Possible null reference return.
                return symbols;
                #pragma warning restore CS8603 // Possible null reference return.
            }
            return null;
        }
    }
}

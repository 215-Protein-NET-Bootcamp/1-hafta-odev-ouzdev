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
        public async Task<ExchangeLatestResponse> LatestCurrency()
        {
            var httpClient = _httpClientFactory.CreateClient("ExchangeRateData");
            var httpResponseMessage = await httpClient.GetAsync($"latest?symbols=&base=TRY");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream =
                   await httpResponseMessage.Content.ReadAsStreamAsync();

                var response = await JsonSerializer.DeserializeAsync<ExchangeLatestResponse>(contentStream);
                if (response != null)
                {
                    return response;

                }
                return new ExchangeLatestResponse();

            }
            else
            {
                return new ExchangeLatestResponse();
            }
        }

        public async Task<SupportedCurrencyResponse> SupportedCurrencies()
        {
            var httpClient = _httpClientFactory.CreateClient("ExchangeRateData");
            var httpResponseMessage = await httpClient.GetAsync($"symbols");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream =
                   await httpResponseMessage.Content.ReadAsStreamAsync();

                var response = await JsonSerializer.DeserializeAsync<SupportedCurrencyResponse>(contentStream);
                if (response != null)
                {
                    return response;

                }
                return new SupportedCurrencyResponse();

            }
            else
            {
                return new SupportedCurrencyResponse();
            }
        }
    }
}

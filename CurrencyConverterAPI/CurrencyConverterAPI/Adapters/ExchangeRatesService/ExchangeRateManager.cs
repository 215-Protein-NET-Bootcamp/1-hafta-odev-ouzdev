using CurrencyConverterAPI.Models;
using CurrencyConverterAPI.Utilities.Result;
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

        public async Task<IDataResult<ExchangeRateResponse>> ConvertCurrency(ConvertCurrencyRate currencyRate)
        {

            var httpClient = _httpClientFactory.CreateClient("ExchangeRateData");
            var httpResponseMessage = await httpClient.GetAsync($"convert?to={currencyRate.To}&from={currencyRate.From}&amount={currencyRate.Amount}");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var parseObject = JsonDocument.Parse(contentStream);
                var result = parseObject.RootElement.GetProperty("result").Deserialize<double>();
                
                return new SuccessDataResult<ExchangeRateResponse>(new ExchangeRateResponse { CalculatedRate = result,From = currencyRate.From,To=currencyRate.To },"Kur Hesaplaması Başarılı");
            }
            
            return new ErrorDataResult<ExchangeRateResponse>("Kur Hesaplanamadı");
        }

        public async Task<IDataResult<ExchangeLatestResponse>> GetLatestCurrency(LatestCurrencyRate latestCurrencyRate)
        {
            var httpClient = _httpClientFactory.CreateClient("ExchangeRateData");
            var httpResponseMessage = await httpClient.GetAsync($"latest?symbols={latestCurrencyRate.Currencies}&base={latestCurrencyRate.BaseCurrency}");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var parseObject = JsonDocument.Parse(contentStream);
                var baseCurrencyResponse = parseObject.RootElement.GetProperty("base").Deserialize<string>();
                var ratesResponse = parseObject.RootElement.GetProperty("rates").Deserialize<IDictionary<string, double>>();

                return new SuccessDataResult<ExchangeLatestResponse>(new ExchangeLatestResponse { Base=baseCurrencyResponse,Rates=ratesResponse }, "Seçilen Kura Göre Kurlar Hesaplandı");

            }

            return new ErrorDataResult<ExchangeLatestResponse>("Kur Hesaplanamadı");
        }

        public async Task<IDataResult<IDictionary<string, string>>> GetSupportedCurrencies()
        {
            var httpClient = _httpClientFactory.CreateClient("ExchangeRateData");
            var httpResponseMessage = await httpClient.GetAsync($"symbols");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                var parseObject = JsonDocument.Parse(contentStream);
                var symbols = parseObject.RootElement.GetProperty("symbols").Deserialize<IDictionary<string, string>>();

                return new SuccessDataResult<IDictionary<string, string>>(symbols,"Desteklenen Kurlar Listelendi");
            }
            return new ErrorDataResult<IDictionary<string, string>>("Kurlar Listelenemedi");
        }
    }
}

namespace CurrencyConverterAPI.Models.Dtos.ConvertCurrencyDtos
{
    public class ExchangeRateResponse
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public double CalculatedRate{ get; set; }
      

    }

}

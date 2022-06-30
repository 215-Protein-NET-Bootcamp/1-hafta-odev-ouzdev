namespace CurrencyConverterAPI.Models
{
    public class GetConvertCurrencyQueryObject
    {
        public string? Amount { get; set; }
        public string? To { get; set; }
        public string? From { get; set; }
    }
}

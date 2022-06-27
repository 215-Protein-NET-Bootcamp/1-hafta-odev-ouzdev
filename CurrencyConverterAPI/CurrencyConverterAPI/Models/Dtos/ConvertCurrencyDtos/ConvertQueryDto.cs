namespace CurrencyConverterAPI.Models.Dtos.ConvertCurrencyDtos
{
    public class ConvertQueryDto
    {
        public double Amount { get; set; }
        public string To { get; set; }
        public string From { get; set; }
    }
}

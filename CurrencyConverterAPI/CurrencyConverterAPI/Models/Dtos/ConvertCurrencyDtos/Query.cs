namespace CurrencyConverterAPI.Models.Dtos.ConvertCurrencyDtos
{
    public class Query
    {
        public int amount { get; set; }
        public string from { get; set; }
        public string to { get; set; }
    }

}

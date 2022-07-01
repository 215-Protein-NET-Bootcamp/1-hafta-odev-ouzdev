namespace CurrencyConverterAPI.Models
{
    public class GetConvertCurrencyRequest
    {
       /// <summary>
       /// For Example: 150.87
       /// </summary>
        public string? Amount { get; set; }
        /// <summary>
        /// For Example: USD
        /// </summary>
        public string? To { get; set; }
        /// <summary>
        /// For Example: TRY
        /// </summary>
        public string? From { get; set; }
    }
}

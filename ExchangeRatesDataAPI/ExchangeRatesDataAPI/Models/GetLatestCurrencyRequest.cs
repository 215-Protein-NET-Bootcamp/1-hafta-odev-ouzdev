namespace CurrencyConverterAPI.Models
{
    public class GetLatestCurrencyRequest
    {
        /// <summary>
        /// For Example: TRY
        /// </summary>
        public string? BaseCurrency { get; set; }
        /// <summary>
        /// For Example: USD or USD,EUR,BTC 
        /// </summary>
        public string? Currencies { get; set; }
    }
}

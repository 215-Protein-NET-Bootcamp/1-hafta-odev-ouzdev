public class ExchangeLatestResponse
{
    public bool success { get; set; }
    public int timestamp { get; set; }
    public string @base { get; set; }
    public string date { get; set; }
    public Rates rates { get; set; }
}
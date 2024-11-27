namespace CryptoRateAPI.Models;

public class CryptoRateResponse
{
    public string CryptoCode { get; set; }
    public List<CurrencyRate> Rates { get; set; }
}

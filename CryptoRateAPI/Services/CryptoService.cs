using CryptoRateAPI.Interfaces;
using CryptoRateAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CryptoRateAPI.Services;

public class CryptoService : ICryptoService
{
    private readonly IRequestServices _requests;

    public CryptoService(IRequestServices requests)
    {
        _requests = requests;
    }
    public async Task<CryptoRateResponse> GetCryptoRatesAsync(string cryptoCode)
    {
        var json = await _requests.CoinMarketLatestQuotes(cryptoCode);
        if (json == null)
            return null;
        decimal usdRate = json["data"][cryptoCode]["quote"]["USD"]["price"].Value<decimal>();

        json = await _requests.ExchangeRatesLatestQuotes();
        if (json == null)
            return null;
        var rates = json["rates"];

        var result = new CryptoRateResponse
        {
            CryptoCode = cryptoCode,
            Rates = new List<CurrencyRate>
            {
                new CurrencyRate { Currency = "USD", Rate = usdRate },
                new CurrencyRate { Currency = "EUR", Rate = usdRate * rates["EUR"].Value<decimal>() },
                new CurrencyRate { Currency = "BRL", Rate = usdRate * rates["BRL"].Value<decimal>() },
                new CurrencyRate { Currency = "GBP", Rate = usdRate * rates["GBP"].Value<decimal>() },
                new CurrencyRate { Currency = "AUD", Rate = usdRate * rates["AUD"].Value<decimal>() }
            }
        };

        return result;
    }
}

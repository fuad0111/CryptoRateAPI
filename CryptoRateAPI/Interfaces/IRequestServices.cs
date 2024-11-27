using Newtonsoft.Json.Linq;

namespace CryptoRateAPI.Interfaces;

public interface IRequestServices
{
    Task<JObject> CoinMarketLatestQuotes(string cryptoCode);

    Task<JObject> ExchangeRatesLatestQuotes();
}

using CryptoRateAPI.Interfaces;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CryptoRateAPI.Services;

public class RequestServices : IRequestServices
{
    private readonly string _coinMarketCapApiKey;
    private readonly string _coinMarketCapApiUrl;
    private readonly string _exchangeRatesApiUrl;

    public RequestServices(IConfiguration configuration)
    {
        _coinMarketCapApiKey = configuration["CoinMarketCapApiKey"];
        _coinMarketCapApiUrl = configuration["CoinMarketCapApiUrl"];
        _exchangeRatesApiUrl = configuration["ExchangeRatesApiUrl"];
    }

    public async Task<JObject> CoinMarketLatestQuotes(string cryptoCode)
    {
        var url = $"{_coinMarketCapApiUrl}{cryptoCode}";


        var headers = new Dictionary<string, string>
        {
          { "X-CMC_PRO_API_KEY", _coinMarketCapApiKey }
        };

        return await RestClientRequestService(url, headers);
    }

    public async Task<JObject> ExchangeRatesLatestQuotes() =>
         await RestClientRequestService(_exchangeRatesApiUrl);

    private async Task<JObject> RestClientRequestService(string url, Dictionary<string, string> headers = null)
    {
        var client = new RestClient(url);
        var request = new RestRequest();

        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
        }

        var response = await client.ExecuteAsync(request);

        if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
        {
            // Log the error or handle it as needed
            return null;
        }

        return JObject.Parse(response.Content);
    }

}

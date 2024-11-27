using CryptoRateAPI.Models;

namespace CryptoRateAPI.Interfaces;

public interface ICryptoService
{
    Task<CryptoRateResponse> GetCryptoRatesAsync(string cryptoCode);
}

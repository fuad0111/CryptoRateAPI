using CryptoRateAPI.Interfaces;
using CryptoRateAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoRateAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CryptoController : ControllerBase
{
    private readonly ICryptoService _cryptoService;

    public CryptoController(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }

    [HttpGet("{cryptoCode}")]
    public async Task<IActionResult> GetCryptoRates(string cryptoCode)
    {
        var rates = await _cryptoService.GetCryptoRatesAsync(cryptoCode.ToUpper());
        if (rates == null)
        {
            return NotFound(new Response<object?>
            {
                Message = "No Data Found.",
                Data = null,
                Success = false
            });
        }

        return Ok(new Response<CryptoRateResponse>
        {
            Message = "Operation done successfully",
            Data = rates,
            Success = true,
        });
    }
}

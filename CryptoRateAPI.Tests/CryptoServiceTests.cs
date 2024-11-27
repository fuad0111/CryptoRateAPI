//using CryptoRateAPI.Services;
//using FluentAssertions;
//using Moq;

//namespace CryptoRateAPI.Tests;

//public class CryptoServiceTests
//{
//    private readonly Mock<IConfiguration> _configurationMock;
//    private readonly ICryptoService _cryptoService;

//    public CryptoServiceTests()
//    {
//        // Mock configuration for API keys
//        _configurationMock = new Mock<IConfiguration>();
//        _configurationMock.Setup(c => c["CoinMarketCapApiKey"]).Returns("TEST_API_KEY");
//        _configurationMock.Setup(c => c["ExchangeRatesApiKey"]).Returns("TEST_API_KEY");

//        // Inject mocked configuration into service
//        _cryptoService = new CryptoService(_configurationMock.Object);
//    }

//    [Fact]
//    public async Task GetCryptoRatesAsync_ShouldReturnRates_WhenCryptoCodeIsValid()
//    {
//        // Arrange
//        string cryptoCode = "BTC";

//        // Act
//        var result = await _cryptoService.GetCryptoRatesAsync(cryptoCode);

//        // Assert
//        result.Should().NotBeNull();
//        result.CryptoCode.Should().Be(cryptoCode);
//        result.Rates.Should().NotBeEmpty()
//            .And.Contain(rate => rate.Currency == "USD")
//            .And.Contain(rate => rate.Currency == "EUR")
//            .And.Contain(rate => rate.Currency == "GBP");
//    }

//    [Fact]
//    public async Task GetCryptoRatesAsync_ShouldReturnNull_WhenCryptoCodeIsInvalid()
//    {
//        // Arrange
//        string invalidCryptoCode = "INVALID";

//        // Act
//        var result = await _cryptoService.GetCryptoRatesAsync(invalidCryptoCode);

//        // Assert
//        result.Should().BeNull();
//    }
//}
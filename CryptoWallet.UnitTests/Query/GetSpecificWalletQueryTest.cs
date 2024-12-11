using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Data.Models;
using CryptoWallet.Logic.Functions.Query;
using CryptoWallet.Logic.Services.Interfaces;
using Moq;

namespace CryptoWallet.UnitTests.Query
{
    public class GetSpecificWalletQueryTest
    {
        private readonly CryptoWalletDbContext _context;
        private readonly GetSpecificWalletQueryHandler _handler;
        private readonly Mock<IBinanceCommunicationService> _binanceService;

        public GetSpecificWalletQueryTest()
        {
            _context = TestDbConnection.CreateContext();
            _binanceService = new Mock<IBinanceCommunicationService>();
            _handler = new GetSpecificWalletQueryHandler(_context, _binanceService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnWalletDto_WhenWalletExists()
        {
            // Arrange
            var walletId = Guid.NewGuid();
            var wallet = new Wallet
            {
                Id = walletId,
                Name = "TestWallet",
                Cryptocurrencies = new List<Cryptocurrency>
                {
                    new Cryptocurrency
                    {
                        Id = Guid.NewGuid(),
                        Name = Data.Models.Enums.CryptoNames.Bitcoin,
                        Value = 0.5M
                    },
                    new Cryptocurrency
                    {
                        Id = Guid.NewGuid(),
                        Name = Data.Models.Enums.CryptoNames.Ethereum,
                        Value = 1.2M
                    }
                }
            };

            _context.Wallets.Add(wallet);
            _context.SaveChanges();

            var query = new GetSpecificWalletQuery(walletId, false);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(wallet.Name, result.Name);
            Assert.Equal(2, result.Currencies.Count);
            Assert.False(result.ConversionSwitched);
        }

        [Fact]
        public async Task Handle_ShouldPerformConversion_WhenConversionIsTrue()
        {
            // Arrange
            var walletId = Guid.NewGuid();
            var wallet = new Wallet
            {
                Id = walletId,
                Name = "TestWallet",
                Cryptocurrencies = new List<Cryptocurrency>
                {
                    new Cryptocurrency
                    {
                        Id = Guid.NewGuid(),
                        Name = Data.Models.Enums.CryptoNames.Bitcoin,
                        Value = 0.5M
                    }
                }
            };

            _context.Wallets.Add(wallet);
            _context.SaveChanges();

            _binanceService.Setup(s => s.Initialize()).Returns(Task.CompletedTask);
            _binanceService.Setup(s => s.GetCurrentPrice(It.IsAny<string>())).ReturnsAsync(50000M);

            var query = new GetSpecificWalletQuery(walletId, true);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(wallet.Name, result.Name);
            Assert.Single(result.Currencies);
            Assert.Equal(50000M, result.Currencies.First().CoinPrice);
            Assert.True(result.ConversionSwitched);

            _binanceService.Verify(s => s.Initialize(), Times.Once);
            _binanceService.Verify(s => s.GetCurrentPrice(It.IsAny<string>()), Times.Exactly(1));
        }
    }
}

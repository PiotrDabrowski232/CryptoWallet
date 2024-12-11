using Azure.Core;
using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Data.Models.Enums;
using CryptoWallet.Data.Models;
using CryptoWallet.Logic.Functions.Command;
using CryptoWallet.Logic.Dto.Crypto;

namespace CryptoWallet.UnitTests.Commands
{
    public class CreateCryptoCommandTest
    {
        private readonly CryptoWalletDbContext _context;
        private readonly CreateCryptoCommandHandler _commandHandler;

        public CreateCryptoCommandTest()
        {
            _context = TestDbConnection.CreateContext();
            _commandHandler = new CreateCryptoCommandHandler(_context);
        }

        [Fact]
        public async Task CreatingCrypto_ReturnNewCryptoDto()
        {
            //Arrange
            var wallet = new Wallet
            {
                Id = Guid.NewGuid(),
                Name = "new Wallet"
            };

            var crypto = new NewCryptoDto
            {
                WalletId = wallet.Id,
                Name = "Bitcoin",
                Value = 0.54m,
            };

            _context.Wallets.Add(wallet);
            _context.SaveChanges();

            //Act
            var command = new CreateCryptoCommand(crypto);
            var result = await _commandHandler.Handle(command, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(crypto.Name, result.Name);
            Assert.Equal(crypto.Value, result.Value);
        }
    }
}

using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Data.Models;
using CryptoWallet.Logic.Dto.Crypto;
using CryptoWallet.Logic.Functions.Command;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.UnitTests.Commands
{
    public class UpdateCryptoCommandTest
    {
        private readonly CryptoWalletDbContext _dbContext;
        private readonly UpdateCryptoCommandHandler _handler;

        public UpdateCryptoCommandTest()
        {
            _dbContext = TestDbConnection.CreateContext();
            _handler = new UpdateCryptoCommandHandler(_dbContext);
        }

        [Fact]
        public async Task UpdateCrypto_ReturnCryptoDto()
        {
            //Arrange
            var wallet = new Wallet()
            {
                Id = Guid.NewGuid(),
                Name="new wallet"
            };
            var crypto = new Cryptocurrency
            {
                Name = Data.Models.Enums.CryptoNames.Bitcoin,
                Value = 0.54M,
                Id = Guid.NewGuid(),
                WalletId = wallet.Id,
            };

            var cryptoToUpdate = new UpdateCryptoDto
            {
                Name = "BNB",
                Value = crypto.Value,
                Id = crypto.Id,
                WalletId = wallet.Id,
            };

            _dbContext.Wallets.Add(wallet);
            _dbContext.CryptoCurrency.Add(crypto);
            _dbContext.SaveChanges();

            _dbContext.Entry(crypto).State = EntityState.Detached;
            _dbContext.Entry(wallet).State = EntityState.Detached;

            //Act
            var command = new UpdateCryptoCommand(cryptoToUpdate);
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(cryptoToUpdate.Name, result.Name);

        }
    }
}

using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Data.Models;
using CryptoWallet.Logic.Functions.Command;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.UnitTests.Commands
{
    public class DeleteCryptoCommandTest
    {
        private readonly CryptoWalletDbContext _context;
        private readonly DeleteCryptoCommandHandler _handler;

        public DeleteCryptoCommandTest()
        {
            _context = TestDbConnection.CreateContext();
            _handler = new DeleteCryptoCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_ShouldDeleteCrypto_WhenCryptoExists()
        {
            // Arrange
            var cryptoId = Guid.NewGuid();
            var crypto = new Cryptocurrency
            {
                Id = cryptoId,
                Name = Data.Models.Enums.CryptoNames.Bitcoin,
                Value = 0.54M,
                WalletId = Guid.NewGuid()
            };

            _context.CryptoCurrency.Add(crypto);
            _context.SaveChanges();

            var existingCrypto = await _context.CryptoCurrency.FirstOrDefaultAsync(c => c.Id == cryptoId);
            Assert.NotNull(existingCrypto);

            // Act
            var command = new DeleteCryptoCommand(cryptoId);
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            var deletedCrypto = await _context.CryptoCurrency.FirstOrDefaultAsync(c => c.Id == cryptoId);
            Assert.Null(deletedCrypto);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenCryptoDoesNotExist()
        {
            // Arrange
            var nonExistentCryptoId = Guid.NewGuid();

            var existingCrypto = await _context.CryptoCurrency.FirstOrDefaultAsync(c => c.Id == nonExistentCryptoId);
            Assert.Null(existingCrypto);

            // Act
            var command = new DeleteCryptoCommand(nonExistentCryptoId);
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
        }
    }
}

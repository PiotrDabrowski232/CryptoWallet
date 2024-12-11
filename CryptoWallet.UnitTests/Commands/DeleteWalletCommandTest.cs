using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Data.Models;
using CryptoWallet.Logic.Functions.Command;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.UnitTests.Commands
{
    public class DeleteWalletCommandTest
    {
        private readonly CryptoWalletDbContext _context;
        private readonly DeleteWalletCommandHandler _handler;

        public DeleteWalletCommandTest()
        {
            _context = TestDbConnection.CreateContext();
            _handler = new DeleteWalletCommandHandler(_context);
        }
        [Fact]
        public async Task Handle_ShouldDeleteWallet_WhenWalletExists()
        {
            // Arrange
            var walletId = Guid.NewGuid();
            var wallet = new Wallet
            {
                Id = walletId,
                Name = "TestWallet"
            };

            _context.Wallets.Add(wallet);
            _context.SaveChanges();

            var existingWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Id == walletId);
            Assert.NotNull(existingWallet);

            // Act
            var command = new DeleteWalletCommand(walletId);
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result); 
            var deletedWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Id == walletId);
            Assert.Null(deletedWallet);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenWalletDoesNotExist()
        {
            // Arrange
            var nonExistentWalletId = Guid.NewGuid();

            var existingWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Id == nonExistentWalletId);
            Assert.Null(existingWallet);

            // Act
            var command = new DeleteWalletCommand(nonExistentWalletId);
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result); 
        }
    }
}


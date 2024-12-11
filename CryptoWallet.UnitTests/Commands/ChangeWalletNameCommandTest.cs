using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Data.Models;
using CryptoWallet.Logic.Exceptions;
using CryptoWallet.Logic.Functions.Command;
using CryptoWallet.Logic.Services;
using Moq;
using System;

namespace CryptoWallet.UnitTests.Commands
{
    public class ChangeWalletNameCommandTest
    {
        private readonly ChangeWalletNameCommandHandler _handler;
        private readonly CryptoWalletDbContext _dbContext;
        private readonly Mock<IWalletValidationService> _valetValidationService;
        public ChangeWalletNameCommandTest()
        {
            _dbContext = TestDbConnection.CreateContext();
            _valetValidationService = new Mock<IWalletValidationService>();
            _handler = new ChangeWalletNameCommandHandler(_dbContext, _valetValidationService.Object);
        }

        [Fact]
        public async Task UpdateWallet_ReturnTrue_WhenNoValidationErrors()
        {
            //Arrange
            var wallet = new Wallet
            {
                Id = Guid.NewGuid(),
                Name = "New Wallet",
            };

            var walletTOUpdate = new
            {
                Id = wallet.Id,
                Name = "new Name Wallet"
            };

            _valetValidationService.Setup(x => x.WalletValidation(walletTOUpdate.Name, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _dbContext.Wallets.Add(wallet);
            _dbContext.SaveChanges();

            //Act
            var command = new ChangeWalletNameCommand(walletTOUpdate.Id, walletTOUpdate.Name);
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.NotNull(result);
            _valetValidationService.Verify(v => v.WalletValidation(walletTOUpdate.Name, It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateWallet_ReturnFalse_WhenNoWalletWithProvidedId()
        {
            //Arrange

            var walletTOUpdate = new
            {
                Id = Guid.NewGuid(),
                Name = "new Name Wallet"
            };

            _valetValidationService.Setup(x => x.WalletValidation(walletTOUpdate.Name, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            var command = new ChangeWalletNameCommand(walletTOUpdate.Id, walletTOUpdate.Name);
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.NotNull(result);
            _valetValidationService.Verify(v => v.WalletValidation(walletTOUpdate.Name, It.IsAny<CancellationToken>()), Times.Once);
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateWallet_ThrowExcepiton_WhenNameTooShort()
        {
            //Arrange
            var wallet = new Wallet
            {
                Id = Guid.NewGuid(),
                Name = "New Wallet",
            };

            var walletTOUpdate = new
            {
                Id = wallet.Id,
                Name = "new"
            };

            _valetValidationService
                .Setup(x => x.WalletValidation(walletTOUpdate.Name, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception($"The name is too short {walletTOUpdate.Name}"));

            _dbContext.Wallets.Add(wallet);
            _dbContext.SaveChanges();

            //Act & Assert
            var command = new ChangeWalletNameCommand(walletTOUpdate.Id, walletTOUpdate.Name);

            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, default));
            Assert.Equal($"The name is too short {walletTOUpdate.Name}", exception.Message);
        }

        [Fact]
        public async Task UpdateWallet_ThrowEntityExistException_WhenProvidedNameExistInDataBase()
        {
            //Arrange
            var wallet = new Wallet
            {
                Id = Guid.NewGuid(),
                Name = "New Wallet",
            };

            var walletTOUpdate = new
            {
                Id = wallet.Id,
                Name = "newaaa"
            };

            _valetValidationService
                .Setup(x => x.WalletValidation(walletTOUpdate.Name, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new EntityExistException($"Wallet with given name exist {walletTOUpdate.Name}"));

            _dbContext.Wallets.Add(wallet);
            _dbContext.SaveChanges();

            //Act & Assert
            var command = new ChangeWalletNameCommand(walletTOUpdate.Id, walletTOUpdate.Name);

            var exception = await Assert.ThrowsAsync<EntityExistException>(() => _handler.Handle(command, default));
            Assert.Equal($"Wallet with given name exist {walletTOUpdate.Name}", exception.Message);
        }
    }
}

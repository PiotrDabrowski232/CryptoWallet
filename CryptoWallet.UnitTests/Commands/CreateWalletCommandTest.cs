using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Data.Models;
using CryptoWallet.Logic.Exceptions;
using CryptoWallet.Logic.Functions.Command;
using CryptoWallet.Logic.Services;
using Moq;

namespace CryptoWallet.UnitTests.Commands
{
    public class CreateWalletCommandTest
    {
        private readonly CreateWalletCommandHandler _handler;
        private readonly CryptoWalletDbContext _dbContext;
        private readonly Mock<IWalletValidationService> _valetValidationService;
        public CreateWalletCommandTest()
        {
            _dbContext = TestDbConnection.CreateContext();
            _valetValidationService = new Mock<IWalletValidationService>();
            _handler = new CreateWalletCommandHandler(_dbContext, _valetValidationService.Object);
        }

        [Fact]
        public async Task CreateNewWallet_ReturnIdOfNewWallet_AsString()
        {
            //Arrange
            var wallet = new Wallet { Id = Guid.NewGuid(), Name = "Normal Wallet" };

            _dbContext.Wallets.Add(wallet);
            _dbContext.SaveChanges();

            _valetValidationService
                .Setup(x => x.WalletValidation(wallet.Name, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            var command = new CreateWalletCommand(wallet.Name);
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.NotNull(result);
            var walletFromDb = _dbContext.Wallets.FirstOrDefault(x => x.Id == Guid.Parse(result));
            Assert.Equal(walletFromDb?.Id.ToString(), result);
            Assert.Equal(walletFromDb?.Name, wallet.Name);
        }

        [Fact]
        public async Task CreateNewWalletWithTooShortName_ThrowEnxception()
        {
            //Arrange
            var wallet = new Wallet { Id = Guid.NewGuid(), Name = "aaa" };

            _valetValidationService
                .Setup(x => x.WalletValidation(wallet.Name, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception($"The name is too short {wallet.Name}"));

            //Act & Assert
            var command = new CreateWalletCommand(wallet.Name);
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, default));
            Assert.Equal($"The name is too short {wallet.Name}", exception.Message);
        }

        [Fact]
        public async Task CreateNewWalletWithExistWalletName_ThrowEntityExistException()
        {
            //Arrange
            var wallet = new Wallet { Id = Guid.NewGuid(), Name = "New Wallet" };

            _dbContext.Wallets.Add(wallet);
            _dbContext.SaveChanges();

            _valetValidationService
                .Setup(x => x.WalletValidation(wallet.Name, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new EntityExistException($"Wallet with given name exist {wallet.Name}"));

            //Act & Assert
            var command = new CreateWalletCommand(wallet.Name);
            var exception = await Assert.ThrowsAsync<EntityExistException>(() => _handler.Handle(command, default));
            Assert.Equal($"Wallet with given name exist {wallet.Name}", exception.Message);
        }
    }
}

using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Data.Models;
using CryptoWallet.Logic.Functions.Query;

namespace CryptoWallet.UnitTests.Query
{
    public class GetAllWalletQueryTest
    {
        private readonly CryptoWalletDbContext _context;
        private readonly GetAllWalletsQueryHandler _handler;

       public GetAllWalletQueryTest()
        {
            _context = TestDbConnection.CreateContext();
            _handler = new GetAllWalletsQueryHandler(_context);
        }

        [Fact]
        public async Task Handle_ShouldReturn_AllAdded()
        {
            //Arrange
            var wallets = new List<Wallet>
            {
                new Wallet{Name = "Pierwszy Portfel"},
                new Wallet{Name = "Drugi Portfel"},
                new Wallet{Name = "Trzeci Portfel"},
                new Wallet{Name = "Piaty Portfel"},
            };

            _context.Wallets.AddRange(wallets);
            await _context.SaveChangesAsync();

            //Act
            var command = new GetAllWalletsQuery();
            var result = await _handler.Handle(command, default);

            //Assert

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var walletFromDb = _context.Wallets.FirstOrDefault(x => x.Name == wallets[1].Name);
            Assert.NotNull(walletFromDb);
        }
        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenThereAreNotWallets()
        {
            //Arrange
            

            //Act
            var command = new GetAllWalletsQuery();
            var result = await _handler.Handle(command, default);

            //Assert

            Assert.NotNull(result);
            Assert.Empty(result);
        }


    }
}

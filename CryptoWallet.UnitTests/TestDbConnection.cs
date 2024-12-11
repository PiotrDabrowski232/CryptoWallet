using CryptoWallet.Data.DatabaseConnection;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.UnitTests
{
    internal class TestDbConnection
    {
        public static CryptoWalletDbContext CreateContext()
        {
            var uniqueDatabaseName = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<CryptoWalletDbContext>()
                .UseInMemoryDatabase(databaseName: uniqueDatabaseName).Options;

            var dbContext = new CryptoWalletDbContext(options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}

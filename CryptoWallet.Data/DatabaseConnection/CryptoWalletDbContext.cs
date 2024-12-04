using CryptoWallet.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.Data.DatabaseConnection
{
    public class CryptoWalletDbContext : DbContext
    {
        public CryptoWalletDbContext(DbContextOptions<CryptoWalletDbContext> options) : base(options) { }

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Cryptocurrency> CryptoCurrency { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CryptoWalletDbContext).Assembly);
        }
    }
}

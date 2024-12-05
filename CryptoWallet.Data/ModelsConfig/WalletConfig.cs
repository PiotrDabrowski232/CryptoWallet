using CryptoWallet.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoWallet.Data.ModelsConfig
{
    public class WalletConfig : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Cryptocurrencies)
                .WithOne(x => x.Wallet)
                .HasForeignKey(x => x.WalletId);
        }
    }
}

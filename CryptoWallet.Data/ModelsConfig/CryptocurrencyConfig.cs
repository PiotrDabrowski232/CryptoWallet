using CryptoWallet.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoWallet.Data.ModelsConfig
{
    public class CryptocurrencyConfig : IEntityTypeConfiguration<Cryptocurrency>
    {
        public void Configure(EntityTypeBuilder<Cryptocurrency> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Value)
                .HasColumnType("decimal(27, 15)");
        }
    }
}

using CryptoWallet.Data.Models.Enums;

namespace CryptoWallet.Data.Models
{
    public class Cryptocurrency
    {
        public Guid Id { get; set; }
        public CryptoNames Name { get; set; }
        public decimal Value { get; set; }

        public Guid WalletId { get; set; }
        public virtual Wallet Wallet { get; set; }
    }
}

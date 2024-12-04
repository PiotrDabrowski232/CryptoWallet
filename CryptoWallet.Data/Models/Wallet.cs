namespace CryptoWallet.Data.Models
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Cryptocurrency> Cryptocurrencies { get; set; }
    }
}

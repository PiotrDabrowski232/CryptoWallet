namespace CryptoWallet.Logic.Dto.Crypto
{
    public class NewCryptoDto
    {
        public Guid WalletId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}

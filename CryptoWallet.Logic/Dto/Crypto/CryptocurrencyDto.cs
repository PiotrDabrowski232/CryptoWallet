namespace CryptoWallet.Logic.Dto.Crypto
{
    public class CryptocurrencyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
    }
}

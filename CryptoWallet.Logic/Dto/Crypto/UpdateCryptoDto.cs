namespace CryptoWallet.Logic.Dto.Crypto
{
    public class UpdateCryptoDto
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}

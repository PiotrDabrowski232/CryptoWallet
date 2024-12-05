using CryptoWallet.Logic.Dto.Crypto;

namespace CryptoWallet.Logic.Dto.Wallet
{
    public class WalletDto
    {
        public string Name { get; set; }
        public IList<CryptocurrencyDto>? Currencies { get; set; }
    }
}

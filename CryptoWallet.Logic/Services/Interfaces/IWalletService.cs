namespace CryptoWallet.Logic.Services
{
    public interface IWalletService
    {
        public Task WalletValidation(string name, CancellationToken ct);
    }
}

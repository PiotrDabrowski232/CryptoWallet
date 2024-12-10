namespace CryptoWallet.Logic.Services
{
    public interface IWalletValidationService
    {
        public Task WalletValidation(string name, CancellationToken ct);
    }
}

namespace CryptoWallet.Logic.Services.Interfaces
{
    public interface IBinanceCommunicationService
    {
        public Task<decimal> GetCurrentPrice(string Coin);
        public Task Initialize();
    }
}

namespace CryptoWallet.Logic.Exceptions
{
    public class EntityExistException : Exception
    {
        public EntityExistException() { }
        public EntityExistException(string message) : base(message) { }
    }
}

using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Logic.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.Logic.Services
{
    public class WalletValidationService : IWalletValidationService
    {
        private readonly CryptoWalletDbContext _dbContext;
        public WalletValidationService(CryptoWalletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task WalletValidation(string name, CancellationToken ct)
        {
            if (name.Length <= 5)
                throw new Exception($"The name is too short {name}");

            var nameExist = await _dbContext.Wallets.AnyAsync(x => x.Name.ToUpper() == name.ToUpper(), ct);

            if (nameExist)
                throw new EntityExistException($"Wallet with given name exist {name}");
        }
    }
}

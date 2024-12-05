using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Logic.Exceptions;
using CryptoWallet.Logic.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.Logic.Functions.Command
{
    public class ChangeWalletNameCommand(string currentName, string newWalletName) : IRequest<bool>
    {
        public string CurrentName { get; set; } = currentName;
        public string NewWalletName { get; set; } = newWalletName;
    }

    public class ChangeWalletNameCommandHandler : BaseRequestHandler<ChangeWalletNameCommand, bool>
    {
        private readonly IWalletService _walletService;
        public ChangeWalletNameCommandHandler(CryptoWalletDbContext context, IWalletService walletService) : base(context)
        {
            _walletService = walletService;
        }

        public override async Task<bool> Handle(ChangeWalletNameCommand request, CancellationToken cancellationToken)
        {
            await _walletService.WalletValidation(request.NewWalletName, cancellationToken);

            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.Name.ToUpper() == request.CurrentName.ToUpper());

            if (wallet == null)
                return false;

            wallet.Name = request.NewWalletName;

            _context.SaveChanges();

            return true;
        }
    }
}

using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Logic.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.Logic.Functions.Command
{
    public class ChangeWalletNameCommand(Guid id, string newWalletName) : IRequest<bool>
    {
        public Guid Id { get; set; } = id;
        public string NewWalletName { get; set; } = newWalletName;
    }

    public class ChangeWalletNameCommandHandler : BaseRequestHandler<ChangeWalletNameCommand, bool>
    {
        private readonly IWalletValidationService _walletService;
        public ChangeWalletNameCommandHandler(CryptoWalletDbContext context, IWalletValidationService walletService) : base(context)
        {
            _walletService = walletService;
        }

        public override async Task<bool> Handle(ChangeWalletNameCommand request, CancellationToken cancellationToken)
        {
            await _walletService.WalletValidation(request.NewWalletName, cancellationToken);

            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (wallet == null)
                return false;

            wallet.Name = request.NewWalletName;

            _context.SaveChanges();

            return true;
        }
    }
}

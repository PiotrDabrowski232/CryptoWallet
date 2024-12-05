using CryptoWallet.Data.DatabaseConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.Logic.Functions.Command
{
    public class DeleteWalletCommand(Guid id) : IRequest<bool>
    {
        public Guid Id { get; set; } = id;
    }

    public class DeleteWalletCommandHandler : BaseRequestHandler<DeleteWalletCommand, bool>
    {
        public DeleteWalletCommandHandler(CryptoWalletDbContext context) : base(context)
        {
        }

        public override async Task<bool> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _context.Wallets
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (wallet == null)
                return false;

            _context.Wallets.Remove(wallet);

            _context.SaveChanges();

            return true;
        }
    }
}

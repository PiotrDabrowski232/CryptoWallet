using CryptoWallet.Data.DatabaseConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.Logic.Functions.Command
{
    public class DeleteCryptoCommand(Guid id) : IRequest<bool>
    {
        public Guid Id { get; set; } = id;
    }

    public class DeleteCryptoCommandHandler : BaseRequestHandler<DeleteCryptoCommand, bool>
    {
        public DeleteCryptoCommandHandler(CryptoWalletDbContext context) : base(context)
        {
        }

        public override async Task<bool> Handle(DeleteCryptoCommand request, CancellationToken cancellationToken)
        {
            var crypto = await _context.CryptoCurrency.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (crypto == null)
                return false;

            _context.CryptoCurrency.Remove(crypto);

            _context.SaveChanges();

            return true;
        }
    }
}

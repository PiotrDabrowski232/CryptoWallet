using CryptoWallet.Data.DatabaseConnection;
using MediatR;

namespace CryptoWallet.Logic.Functions
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected readonly CryptoWalletDbContext _context;
        protected BaseRequestHandler(CryptoWalletDbContext context)
        {
            _context = context;
        }
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}

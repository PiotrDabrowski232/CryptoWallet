using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Logic.Dto.Crypto;
using CryptoWallet.Logic.Dto.Wallet;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.Logic.Functions.Query
{
    public class GetSpecificWalletQuery(Guid id) : IRequest<WalletDto>
    {
        public Guid Id { get; set; } = id;
    }

    public class GetSpecificWalletQueryHandler : BaseRequestHandler<GetSpecificWalletQuery, WalletDto>
    {
        public GetSpecificWalletQueryHandler(CryptoWalletDbContext context) : base(context)
        {
        }

        public override async Task<WalletDto> Handle(GetSpecificWalletQuery request, CancellationToken cancellationToken)
        {
            return await _context.Wallets.Where(x => x.Id == request.Id)
                .Select(x => new WalletDto
                {
                    Name = x.Name,
                    Currencies = x.Cryptocurrencies.Select(y => new CryptocurrencyDto
                    {
                        Name = Enum.GetName(y.Name),
                        Value = y.Value,
                    }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken) ?? new WalletDto();
        }
    }
}

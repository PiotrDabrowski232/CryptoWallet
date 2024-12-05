using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Logic.Dto.Wallet;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.Logic.Functions.Query
{
    public class GetAllWalletsQuery : IRequest<IEnumerable<WalletBasicInfoDto>>;

    public class GetAllWalletsQueryHandler : BaseRequestHandler<GetAllWalletsQuery, IEnumerable<WalletBasicInfoDto>>
    {
        public GetAllWalletsQueryHandler(CryptoWalletDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<WalletBasicInfoDto>> Handle(GetAllWalletsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Wallets
                .Select(x => new WalletBasicInfoDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    CryptoCount = x.Cryptocurrencies.Count,
                }
                ).ToListAsync(cancellationToken);
        }
    }
}

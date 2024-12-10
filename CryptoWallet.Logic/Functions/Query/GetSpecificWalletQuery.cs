using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Logic.Dto.Crypto;
using CryptoWallet.Logic.Dto.Wallet;
using CryptoWallet.Logic.Extensions;
using CryptoWallet.Logic.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace CryptoWallet.Logic.Functions.Query
{
    public class GetSpecificWalletQuery(Guid id, bool? conversion) : IRequest<WalletDto>
    {
        public Guid Id { get; set; } = id;
        public bool? Conversion { get; set; } = conversion;
    }

    public class GetSpecificWalletQueryHandler : BaseRequestHandler<GetSpecificWalletQuery, WalletDto>
    {
        private readonly IBinanceCommunicationService _binanceService;
        public GetSpecificWalletQueryHandler(CryptoWalletDbContext context, IBinanceCommunicationService binanceService) : base(context)
        {
            _binanceService = binanceService;
        }

        public override async Task<WalletDto> Handle(GetSpecificWalletQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Wallets.Where(x => x.Id == request.Id)
                .Select(x => new WalletDto
                {
                    Name = x.Name,
                    Currencies = x.Cryptocurrencies.Select(y => new CryptocurrencyDto
                    {
                        Id = y.Id,
                        Name = Enum.GetName(y.Name),
                        Value = y.Value,
                        Description = y.Name.GetEnumDescription()
                    }).ToList(),
                    ConversionSwitched = false,
                })
                .FirstOrDefaultAsync(cancellationToken) ?? new WalletDto();

            if (request.Conversion == true)
            {
                await _binanceService.Initialize();

                result.Currencies = await Task.WhenAll(
                    result.Currencies.Select(async x =>
                    {
                        x.CoinPrice = await _binanceService.GetCurrentPrice(x.Description);
                        return x;
                    })
                );
                result.ConversionSwitched = true;
            }


            return result;
        }
    }
}

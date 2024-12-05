using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Data.Models.Enums;
using CryptoWallet.Logic.Dto.Crypto;
using MediatR;

namespace CryptoWallet.Logic.Functions.Command
{
    public class UpdateCryptoCommand(UpdateCryptoDto crypto) : IRequest<CryptocurrencyDto>
    {
        public UpdateCryptoDto Crypto { get; set; } = crypto;
    }

    public class UpdateCryptoCommandHandler : BaseRequestHandler<UpdateCryptoCommand, CryptocurrencyDto>
    {
        public UpdateCryptoCommandHandler(CryptoWalletDbContext context) : base(context)
        {
        }

        public override async Task<CryptocurrencyDto> Handle(UpdateCryptoCommand request, CancellationToken cancellationToken)
        {
            var crypto = new Data.Models.Cryptocurrency
            {
                Name = (CryptoNames)Enum.Parse(typeof(CryptoNames), request.Crypto.Name),
                Value = request.Crypto.Value,
                Id = request.Crypto.Id,
                WalletId = request.Crypto.WalletId
            };

            _context.CryptoCurrency.Update(crypto);

            await _context.SaveChangesAsync();

            return new CryptocurrencyDto
            {
                Name =  request.Crypto.Name,
                Value = request.Crypto.Value,
            };
        }
    }
}

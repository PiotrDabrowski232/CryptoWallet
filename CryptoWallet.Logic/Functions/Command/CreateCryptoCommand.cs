using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Data.Models;
using CryptoWallet.Data.Models.Enums;
using CryptoWallet.Logic.Dto.Crypto;
using CryptoWallet.Logic.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.Logic.Functions.Command
{
    public class CreateCryptoCommand(NewCryptoDto crypto) : IRequest<CryptocurrencyDto>
    {
        public NewCryptoDto Crypto { get; set; } = crypto;
    }

    public class CreateCryptoCommandHandler : BaseRequestHandler<CreateCryptoCommand, CryptocurrencyDto>
    {
        public CreateCryptoCommandHandler(CryptoWalletDbContext context) : base(context)
        {
        }

        public override async Task<CryptocurrencyDto> Handle(CreateCryptoCommand request, CancellationToken cancellationToken)
        {
            var crypto = new Cryptocurrency
            {
                WalletId = request.Crypto.WalletId,
                Name = (CryptoNames)Enum.Parse(typeof(CryptoNames), request.Crypto.Name),
                Value = request.Crypto.Value
            };

            _context.CryptoCurrency.Add(crypto);

            _context.SaveChanges();

            return new CryptocurrencyDto
            {
                Name = request.Crypto.Name,
                Value = request.Crypto.Value
            };
        }
    }
}

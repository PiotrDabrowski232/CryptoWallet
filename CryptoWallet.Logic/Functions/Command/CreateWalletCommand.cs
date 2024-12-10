using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Data.Models;
using CryptoWallet.Logic.Exceptions;
using CryptoWallet.Logic.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.Logic.Functions.Command
{
    public class CreateWalletCommand(string walletName) : IRequest<string>
    {
        public string WalletName { get; set; } = walletName;
    }

    public class CreateWalletCommandHandler : BaseRequestHandler<CreateWalletCommand, string>
    {
        private readonly IWalletValidationService _walletService;
        public CreateWalletCommandHandler(CryptoWalletDbContext context, IWalletValidationService walletService) : base(context)
        {
            _walletService = walletService;
        }

        public override async Task<string> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            await _walletService.WalletValidation(request.WalletName, cancellationToken);

            var wallet = new Wallet()
            {
                Id = Guid.NewGuid(),
                Name = request.WalletName,
            };

            _context.Add(wallet);

            _context.SaveChanges();

            return wallet.Id.ToString();
        }
    }
}

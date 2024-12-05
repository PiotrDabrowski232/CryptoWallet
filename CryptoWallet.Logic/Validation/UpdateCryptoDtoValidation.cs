using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.Data.Models.Enums;
using CryptoWallet.Logic.Dto.Crypto;
using FluentValidation;

namespace CryptoWallet.Logic.Validation
{
    public class UpdateCryptoDtoValidation : AbstractValidator<UpdateCryptoDto>
    {
        public UpdateCryptoDtoValidation() { }
        public UpdateCryptoDtoValidation(CryptoWalletDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name can not be empty")
                .NotNull()
                .WithMessage("Name can not be empty");

            RuleFor(x => x.Name)
                .Custom((value, context) =>
                {
                    if (value != null && value.Length > 0)
                    {

                        var walletId = context.InstanceToValidate.WalletId;
                        var cryptoId = context.InstanceToValidate.Id;

                        var nameExist = dbContext.CryptoCurrency
                        .Any(x => x.WalletId == walletId
                        && x.Name == (CryptoNames)Enum.Parse(typeof(CryptoNames), value) 
                        && x.Id != cryptoId);

                        if (nameExist)
                            context.AddFailure("Name", $"Cryptocurrency with name '{value}' already exists for this wallet.");
                    }
                });

            RuleFor(x => x.Value)
                .GreaterThan(0)
                .WithMessage("Value should be grater than 0");
        }
    }
}

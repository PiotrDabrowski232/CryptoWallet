using CryptoWallet.Data.Models.Enums;
using MediatR;

namespace CryptoWallet.Logic.Functions.Query
{
    public class GetAllCryptocurrencyNames : IRequest<IList<string>>;

    public class GetAllCryptocurrencyNamesHandler : IRequestHandler<GetAllCryptocurrencyNames, IList<string>>
    {
        public Task<IList<string>> Handle(GetAllCryptocurrencyNames request, CancellationToken cancellationToken)
        {
            var names = Enum.GetNames(typeof(CryptoNames)).ToList();

            return Task.FromResult<IList<string>>(names);
        }
    }
}

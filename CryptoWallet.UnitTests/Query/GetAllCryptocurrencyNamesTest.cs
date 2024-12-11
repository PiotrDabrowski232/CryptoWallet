using CryptoWallet.Data.Models.Enums;
using CryptoWallet.Logic.Functions.Query;

namespace CryptoWallet.UnitTests.Query
{
    public class GetAllCryptocurrencyNamesTest
    {
        private readonly GetAllCryptocurrencyNamesHandler _handler;

        public GetAllCryptocurrencyNamesTest()
        {
            _handler = new GetAllCryptocurrencyNamesHandler();
        }

        [Fact]
        public async Task Handler_ShouldReturnListOfStrings_EqualEnumCount()
        {
            //Arrange
            var enums = Enum.GetNames(typeof(CryptoNames)).ToList();

            //Act
            var command = new GetAllCryptocurrencyNames();
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(enums.Count(), result.Count());
        }
    }
}

using CryptoWallet.Logic.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace CryptoWallet.Logic.Services
{
    public class BinanceCommunicationService(HttpClient httpClient) : IBinanceCommunicationService
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<decimal> GetCurrentPrice(string Coin)
        {
            string binanceUrl = $"https://api.binance.com/api/v3/ticker/price?symbol={Coin}PLN";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(binanceUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);

                return decimal.Parse(json["price"].ToString(), CultureInfo.InvariantCulture);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

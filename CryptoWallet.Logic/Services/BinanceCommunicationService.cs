using CryptoWallet.Logic.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace CryptoWallet.Logic.Services
{
    public class BinanceCommunicationService : IBinanceCommunicationService
    {
        private readonly HttpClient _httpClient;
        private decimal USDTPLN;
        public BinanceCommunicationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Initialize()
        {
            USDTPLN = await ConvertToPPLN();
        }


        public async Task<decimal> GetCurrentPrice(string Coin)
        {
            string cryptoPriceUrl = $"https://api.binance.com/api/v3/ticker/price?symbol={Coin}USDT";
            try
            {
                if(Coin == "USDT")
                    return USDTPLN;
                
                HttpResponseMessage response = await _httpClient.GetAsync(cryptoPriceUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);

                return decimal.Parse(json["price"].ToString(), CultureInfo.InvariantCulture) * USDTPLN;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<decimal> ConvertToPPLN()
        {
            string conversionToPLN = $"https://api.binance.com/api/v3/ticker/price?symbol=USDTPLN";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(conversionToPLN);
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

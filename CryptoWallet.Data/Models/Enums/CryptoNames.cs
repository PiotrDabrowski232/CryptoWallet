using System.ComponentModel;

namespace CryptoWallet.Data.Models.Enums
{
    public enum CryptoNames
    {
        [Description("BTC")]
        Bitcoin,

        [Description("ETH")]
        Ethereum,

        [Description("BNB")]
        BNB,

        [Description("USDT")]
        TetherUS,

        [Description("XRP")]
        XRP,

        [Description("DOGE")]
        DogeCoin,

        [Description("ADA")]
        Cardano,

        [Description("SHIB")]
        SHIBAINU,

        [Description("DOT")]
        Polkadot,
    }
}

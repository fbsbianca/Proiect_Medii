using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazinOnline.Models
{
    public class CryptoMarketCap
    {
        public int ID { get; set; }
        public int CryptocurrencyID { get; set; }
        public Cryptocurrency Cryptocurrency { get; set; }
        public int MarketCapID { get; set; }
        public MarketCap MarketCap { get; set; }
    }
}

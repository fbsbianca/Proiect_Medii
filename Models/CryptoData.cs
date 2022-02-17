using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazinOnline.Models
{
    public class CryptoData
    {
        public IEnumerable<Cryptocurrency> Cryptocurrencies { get; set; }
        public IEnumerable<MarketCap> MarketCaps { get; set; }
        public IEnumerable<CryptoMarketCap> CryptoMarketCaps { get; set; }
    }
}

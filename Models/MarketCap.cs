using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazinOnline.Models
{
    public class MarketCap
    {
        public int ID { get; set; }
        public string MarketCapValue { get; set; }
        public ICollection<CryptoMarketCap> CryptoMarketCaps{ get; set; }
    }
}

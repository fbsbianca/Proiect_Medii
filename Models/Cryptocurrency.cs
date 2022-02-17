using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazinOnline.Models
{
    public class Cryptocurrency
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }

        public int SellerID { get; set; }
        public Seller Seller { get; set; }

        public ICollection<CryptoMarketCap> CryptoMarketCaps{ get; set; }
    }
}

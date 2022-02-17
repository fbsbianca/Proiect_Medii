using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazinOnline.Models
{
    public class Seller
    {
        public int ID { get; set; }
        public string SellerName { get; set; }
        public ICollection<Cryptocurrency> Cryptocurrencies { get; set; }
    }
}

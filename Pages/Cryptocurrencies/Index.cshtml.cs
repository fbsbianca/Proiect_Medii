using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MagazinOnline.Data;
using MagazinOnline.Models;

namespace MagazinOnline.Pages.Cryptocurrencies
{
    public class IndexModel : PageModel
    {
        private readonly MagazinOnline.Data.MagazinOnlineContext _context;

        public IndexModel(MagazinOnline.Data.MagazinOnlineContext context)
        {
            _context = context;
        }

        public IList<Cryptocurrency> Cryptocurrency { get;set; }

        public CryptoData CryptoD { get; set; }
        public int CryptocurrencyID { get; set; }
        public int MarketCapID { get; set; }

        public async Task OnGetAsync(int? id, int? MarketCapID)
        {
            CryptoD = new CryptoData();
            
            CryptoD.Cryptocurrencies = await _context.Cryptocurrency
               .Include(b => b.Seller)
               .Include(b => b.CryptoMarketCaps)
               .ThenInclude(b => b.MarketCap)
               .AsNoTracking()
               .OrderBy (b => b.Name )
               .ToListAsync();

            if (id != null)
            {
                CryptocurrencyID = id.Value; 
                Cryptocurrency cryptocurrency = CryptoD.Cryptocurrencies.Where(i => i.ID == id.Value).Single();
                CryptoD.MarketCaps = cryptocurrency.CryptoMarketCaps.Select(s => s.MarketCap);
            }
        }
    }
}

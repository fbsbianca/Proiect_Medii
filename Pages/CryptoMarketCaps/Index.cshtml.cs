using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MagazinOnline.Data;
using MagazinOnline.Models;

namespace MagazinOnline.Pages.CryptoMarketCaps
{
    public class IndexModel : PageModel
    {
        private readonly MagazinOnline.Data.MagazinOnlineContext _context;

        public IndexModel(MagazinOnline.Data.MagazinOnlineContext context)
        {
            _context = context;
        }

        public IList<CryptoMarketCap> CryptoMarketCap { get;set; }

        public async Task OnGetAsync()
        {
            CryptoMarketCap = await _context.CryptoMarketCap
                .Include(c => c.Cryptocurrency)
                .Include(c => c.MarketCap).ToListAsync();
        }
    }
}

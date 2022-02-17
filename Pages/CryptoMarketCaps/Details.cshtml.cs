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
    public class DetailsModel : PageModel
    {
        private readonly MagazinOnline.Data.MagazinOnlineContext _context;

        public DetailsModel(MagazinOnline.Data.MagazinOnlineContext context)
        {
            _context = context;
        }

        public CryptoMarketCap CryptoMarketCap { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CryptoMarketCap = await _context.CryptoMarketCap
                .Include(c => c.Cryptocurrency)
                .Include(c => c.MarketCap).FirstOrDefaultAsync(m => m.ID == id);

            if (CryptoMarketCap == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

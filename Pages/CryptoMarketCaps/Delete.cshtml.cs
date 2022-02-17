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
    public class DeleteModel : PageModel
    {
        private readonly MagazinOnline.Data.MagazinOnlineContext _context;

        public DeleteModel(MagazinOnline.Data.MagazinOnlineContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CryptoMarketCap = await _context.CryptoMarketCap.FindAsync(id);

            if (CryptoMarketCap != null)
            {
                _context.CryptoMarketCap.Remove(CryptoMarketCap);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

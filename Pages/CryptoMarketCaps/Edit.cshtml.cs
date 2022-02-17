using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MagazinOnline.Data;
using MagazinOnline.Models;

namespace MagazinOnline.Pages.CryptoMarketCaps
{
    public class EditModel : PageModel
    {
        private readonly MagazinOnline.Data.MagazinOnlineContext _context;

        public EditModel(MagazinOnline.Data.MagazinOnlineContext context)
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
           ViewData["CryptocurrencyID"] = new SelectList(_context.Cryptocurrency, "ID", "ID");
           ViewData["MarketCapID"] = new SelectList(_context.Set<MarketCap>(), "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CryptoMarketCap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CryptoMarketCapExists(CryptoMarketCap.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CryptoMarketCapExists(int id)
        {
            return _context.CryptoMarketCap.Any(e => e.ID == id);
        }
    }
}

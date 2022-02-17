using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MagazinOnline.Data;
using MagazinOnline.Models;

namespace MagazinOnline.Pages.CryptoMarketCaps
{
    public class CreateModel : PageModel
    {
        private readonly MagazinOnline.Data.MagazinOnlineContext _context;

        public CreateModel(MagazinOnline.Data.MagazinOnlineContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CryptocurrencyID"] = new SelectList(_context.Cryptocurrency, "ID", "ID");
        ViewData["MarketCapID"] = new SelectList(_context.Set<MarketCap>(), "ID", "ID");
            return Page();
        }

        [BindProperty]
        public CryptoMarketCap CryptoMarketCap { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CryptoMarketCap.Add(CryptoMarketCap);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

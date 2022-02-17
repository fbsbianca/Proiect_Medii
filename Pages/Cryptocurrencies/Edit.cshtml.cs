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

namespace MagazinOnline.Pages.Cryptocurrencies
{
    public class EditModel : MarketCapValues
    {
        private readonly MagazinOnline.Data.MagazinOnlineContext _context;

        public EditModel(MagazinOnline.Data.MagazinOnlineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cryptocurrency Cryptocurrency { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cryptocurrency = await _context.Cryptocurrency
                .Include(b => b.Seller)
                .Include(b => b.CryptoMarketCaps).ThenInclude(b => b.MarketCap)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Cryptocurrency == null)
            {
                return NotFound();
            }

            PopulateAssignedCategoryData(_context, Cryptocurrency);

            ViewData["SellerID"] = new SelectList(_context.Set<Seller>(), "ID", "SellerName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedMarketCaps)
        {
            if (id== null)
            {
                return NotFound();
            }

            var criptocurrencyToUpdate = await _context.Cryptocurrency
                .Include(i => i.Seller)
                .Include(i => i.CryptoMarketCaps).ThenInclude(i => i.MarketCap)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (criptocurrencyToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Cryptocurrency>(
                criptocurrencyToUpdate,
                "Cryptocurrency",
                i => i.Name, i => i.Code,
                i => i.Price, i => i.Seller))
            {
                UpdateCryptoMarketCap(_context, selectedMarketCaps, criptocurrencyToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateCryptoMarketCap(_context, selectedMarketCaps, criptocurrencyToUpdate);
            PopulateAssignedCategoryData(_context, criptocurrencyToUpdate);
            return Page();
        }

        private bool CryptocurrencyExists(int id)
        {
            return _context.Cryptocurrency.Any(e => e.ID == id);
        }
    }
}

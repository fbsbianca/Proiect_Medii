using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MagazinOnline.Data;
using MagazinOnline.Models;

namespace MagazinOnline.Pages.Cryptocurrencies
{
    public class CreateModel : MarketCapValues
    {
        private readonly MagazinOnline.Data.MagazinOnlineContext _context;

        public CreateModel(MagazinOnline.Data.MagazinOnlineContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["SellerID"] = new SelectList(_context.Set<Seller>(), "ID", "SellerName");
            var cryptocurrency = new Cryptocurrency();
            cryptocurrency.CryptoMarketCaps = new List<CryptoMarketCap>();
            PopulateAssignedCategoryData(_context, cryptocurrency);
            return Page();
        }

        [BindProperty]
        public Cryptocurrency Cryptocurrency { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string [] selectedMarketCaps)
        {
            var newCryptocurrency = new Cryptocurrency();
            if (selectedMarketCaps != null)
            {
                newCryptocurrency.CryptoMarketCaps = new List<CryptoMarketCap>();
                foreach (var cat in selectedMarketCaps)
                {
                    var catToAdd = new CryptoMarketCap
                    {
                        MarketCapID = int.Parse(cat)
                    };
                    newCryptocurrency.CryptoMarketCaps.Add(catToAdd);

                }
            }

            if (await TryUpdateModelAsync<Cryptocurrency>
                (newCryptocurrency, 
                "Cryptocurrency", 
                i => i.Name, i => i.Code,
                i => i.Price,  i => i.SellerID))

            { 
                _context.Cryptocurrency.Add(Cryptocurrency);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateAssignedCategoryData(_context, newCryptocurrency); 
            return Page();
        }
    }
}

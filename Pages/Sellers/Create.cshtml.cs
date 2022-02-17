using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MagazinOnline.Data;
using MagazinOnline.Models;

namespace MagazinOnline.Pages.Sellers
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
            return Page();
        }

        [BindProperty]
        public Seller Seller { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Seller.Add(Seller);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

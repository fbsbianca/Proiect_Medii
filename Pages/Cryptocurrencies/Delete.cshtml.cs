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
    public class DeleteModel : PageModel
    {
        private readonly MagazinOnline.Data.MagazinOnlineContext _context;

        public DeleteModel(MagazinOnline.Data.MagazinOnlineContext context)
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

            Cryptocurrency = await _context.Cryptocurrency.FirstOrDefaultAsync(m => m.ID == id);

            if (Cryptocurrency == null)
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

            Cryptocurrency = await _context.Cryptocurrency.FindAsync(id);

            if (Cryptocurrency != null)
            {
                _context.Cryptocurrency.Remove(Cryptocurrency);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

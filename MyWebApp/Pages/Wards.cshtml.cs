using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using MyWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApp.Pages
{
    public class WardsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public WardsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Ward> Wards { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Wards != null)
            {
                Wards = await _context.Wards.AsNoTracking().ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var ward = await _context.Wards.FindAsync(id);

            if (ward != null)
            {
                _context.Wards.Remove(ward);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Wards");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using MyWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApp.Pages
{
    public class DoctorsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DoctorsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Doctor> Doctors { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Doctors != null)
            {
                Doctors = await _context.Doctors.AsNoTracking().ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Doctors");
        }
    }
}
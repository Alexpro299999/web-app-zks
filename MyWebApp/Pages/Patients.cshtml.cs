using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using MyWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApp.Pages
{
    public class PatientsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PatientsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Patient> Patients { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Patients != null)
            {
                Patients = await _context.Patients
                    .Include(p => p.Ward)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Patients");
        }
    }
}
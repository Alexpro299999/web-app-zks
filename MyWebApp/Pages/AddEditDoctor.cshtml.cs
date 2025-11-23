using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWebApp.Data;
using MyWebApp.Models;
using System.Threading.Tasks;

namespace MyWebApp.Pages
{
    public class AddEditDoctorModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddEditDoctorModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Doctor Doctor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                Doctor = await _context.Doctors.FindAsync(id.Value);
                if (Doctor == null)
                {
                    return NotFound();
                }
            }
            else
            {
                Doctor = new Doctor();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Doctor.Id == 0)
            {
                _context.Doctors.Add(Doctor);
            }
            else
            {
                var existingDoctor = await _context.Doctors.FindAsync(Doctor.Id);
                if (existingDoctor == null)
                {
                    return NotFound();
                }

                existingDoctor.LastName = Doctor.LastName;
                existingDoctor.FirstName = Doctor.FirstName;
                existingDoctor.MiddleName = Doctor.MiddleName;
                existingDoctor.Specialization = Doctor.Specialization;
                existingDoctor.CabinetNumber = Doctor.CabinetNumber;
                existingDoctor.Schedule = Doctor.Schedule;

                _context.Doctors.Update(existingDoctor);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Doctors");
        }
    }
}
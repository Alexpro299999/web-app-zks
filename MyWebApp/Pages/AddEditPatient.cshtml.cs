using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWebApp.Data;
using MyWebApp.Models;
using System.Threading.Tasks;

namespace MyWebApp.Pages
{
    public class AddEditPatientModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddEditPatientModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Patient Patient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                Patient = await _context.Patients.FindAsync(id.Value);
                if (Patient == null)
                {
                    return NotFound();
                }
            }
            else
            {
                Patient = new Patient { BirthDate = System.DateTime.Today };
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Patient.Id == 0)
            {
                _context.Patients.Add(Patient);
            }
            else
            {
                var existingPatient = await _context.Patients.FindAsync(Patient.Id);
                if (existingPatient == null)
                {
                    return NotFound();
                }

                existingPatient.LastName = Patient.LastName;
                existingPatient.FirstName = Patient.FirstName;
                existingPatient.MiddleName = Patient.MiddleName;
                existingPatient.BirthDate = Patient.BirthDate;
                existingPatient.InsuranceNumber = Patient.InsuranceNumber;
                existingPatient.Phone = Patient.Phone;
                existingPatient.Address = Patient.Address;

                _context.Patients.Update(existingPatient);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("/Patients");
        }
    }
}
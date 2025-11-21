using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.Pages
{
    public class AddEditProcedureModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddEditProcedureModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Procedure Procedure { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                Procedure = await _context.Procedures.FindAsync(id.Value);
                if (Procedure == null) return NotFound();
            }
            else
            {
                Procedure = new Procedure();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (Procedure.Id == 0)
            {
                _context.Procedures.Add(Procedure);
            }
            else
            {
                _context.Procedures.Update(Procedure);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Procedures");
        }
    }
}
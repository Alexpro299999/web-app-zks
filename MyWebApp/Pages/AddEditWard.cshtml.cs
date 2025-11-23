using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWebApp.Data;
using MyWebApp.Models;
using System.Threading.Tasks;

namespace MyWebApp.Pages
{
    public class AddEditWardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddEditWardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ward Ward { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                Ward = await _context.Wards.FindAsync(id.Value);
                if (Ward == null)
                {
                    return NotFound();
                }
            }
            else
            {
                Ward = new Ward { Type = "Общая" };
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Ward.Id == 0)
            {
                _context.Wards.Add(Ward);
            }
            else
            {
                var existingWard = await _context.Wards.FindAsync(Ward.Id);
                if (existingWard == null)
                {
                    return NotFound();
                }

                existingWard.Number = Ward.Number;
                existingWard.Floor = Ward.Floor;
                existingWard.Capacity = Ward.Capacity;
                existingWard.Type = Ward.Type;

                _context.Wards.Update(existingWard);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Wards");
        }
    }
}
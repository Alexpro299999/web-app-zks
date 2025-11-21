using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.Pages
{
    public class AddEditReviewModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddEditReviewModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Review Review { get; set; }

        public SelectList ClientList { get; set; }
        public SelectList ProcedureList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                Review = await _context.Reviews.FindAsync(id.Value);
                if (Review == null) return NotFound();
            }
            else
            {
                Review = new Review { Date = DateTime.Now, Rating = 5 };
            }

            await LoadDropdowns();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return Page();
            }

            if (Review.Id == 0)
            {
                _context.Reviews.Add(Review);
            }
            else
            {
                _context.Reviews.Update(Review);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Reviews");
        }

        private async Task LoadDropdowns()
        {
            // Используем Fio для отображения клиента
            var clients = await _context.Clients.ToListAsync();
            ClientList = new SelectList(clients, "Id", "Fio");

            ProcedureList = new SelectList(await _context.Procedures.ToListAsync(), "Id", "Name");
        }
    }
}
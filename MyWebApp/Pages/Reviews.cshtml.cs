using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.Pages
{
    public class ReviewsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReviewsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Review> Reviews { get; set; }

        public async Task OnGetAsync()
        {
            Reviews = await _context.Reviews
                .Include(r => r.Client)
                .Include(r => r.Procedure)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
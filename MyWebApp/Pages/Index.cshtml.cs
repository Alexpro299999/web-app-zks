using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using System.Threading.Tasks;

namespace MyWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int PatientsCount { get; set; }
        public int DoctorsCount { get; set; }
        public int FreeWardsCount { get; set; }

        public async Task OnGetAsync()
        {
            PatientsCount = await _context.Patients.CountAsync();
            DoctorsCount = await _context.Doctors.CountAsync();
            FreeWardsCount = await _context.Wards.CountAsync();
        }
    }
}
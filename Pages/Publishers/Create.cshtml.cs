using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Petean_David_Lab2.Data;
using Petean_David_Lab2.Models; // Aici e definit modelul Publisher

using System.Threading.Tasks;

namespace Petean_David_Lab2.Pages.Publishers
{
    public class CreateModel : PageModel
    {
        private readonly Petean_David_Lab2Context _context;

        public CreateModel(Petean_David_Lab2Context context)
        {
            _context = context;
        }

        // Modificat: Returnează Pagina
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Presupunând că DbSet-ul tău în DbContext se numește Publisher
            _context.Publisher.Add(Publisher);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Petean_David_Lab2.Data;
using Petean_David_Lab2.Models;
using System.Threading.Tasks;

namespace Petean_David_Lab2.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly Petean_David_Lab2Context _context;

        public CreateModel(Petean_David_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["AuthorID"] = new SelectList(_context.Author.Select(a => new
            {
                ID = a.ID,
                FullName = a.FirstName + " " + a.LastName
            }), "ID", "FullName");

            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Book == null || Book == null)
            {
                ViewData["AuthorID"] = new SelectList(_context.Author.Select(a => new
                {
                    ID = a.ID,
                    FullName = a.FirstName + " " + a.LastName
                }), "ID", "FullName");

                ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");

                return Page();
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}


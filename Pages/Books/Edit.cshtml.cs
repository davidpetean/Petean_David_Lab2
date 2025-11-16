using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Petean_David_Lab2.Data;
using Petean_David_Lab2.Models;

namespace Petean_David_Lab2.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly Petean_David_Lab2.Data.Petean_David_Lab2Context _context;

        public EditModel(Petean_David_Lab2.Data.Petean_David_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (book == null)
            {
                return NotFound();
            }
            Book = book;

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
            if (!ModelState.IsValid)
            {
                ViewData["AuthorID"] = new SelectList(_context.Author.Select(a => new
                {
                    ID = a.ID,
                    FullName = a.FirstName + " " + a.LastName
                }), "ID", "FullName");

                ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");
                return Page();
            }

            _context.Attach(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}

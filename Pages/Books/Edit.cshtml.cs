using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Petean_David_Lab2.Models;
using Petean_David_Lab2.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Petean_David_Lab2.Pages.Books
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Petean_David_Lab2Context _context;

        public EditModel(Petean_David_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public List<AssignedCategoryData> AssignedCategoryDataList { get; set; } = new List<AssignedCategoryData>();


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories).ThenInclude(b => b.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }

            AssignedCategoryDataList = PopulateAssignedCategoryData(_context, Book);

            var authorList = _context.Author.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.FirstName
            });

            ViewData["AuthorID"] = new SelectList(authorList, "ID", "FullName", Book.AuthorID);
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName", Book.PublisherID);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Book
            .Include(i => i.BookCategories)
            .FirstOrDefaultAsync(s => s.ID == id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            i => i.Title,
            i => i.AuthorID,
            i => i.Price,
            i => i.PublishingDate,
            i => i.PublisherID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                }
            }

            AssignedCategoryDataList = PopulateAssignedCategoryData(_context, bookToUpdate);

            var authorList = _context.Author.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.FirstName
            });
            ViewData["AuthorID"] = new SelectList(authorList, "ID", "FullName", bookToUpdate.AuthorID);
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName", bookToUpdate.PublisherID);

            return Page();
        }
    }
}

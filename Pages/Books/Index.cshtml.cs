using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Petean_David_Lab2.Data;
using Petean_David_Lab2.Models;
using Petean_David_Lab2.ViewModels;

namespace Petean_David_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Petean_David_Lab2.Data.Petean_David_Lab2Context _context;

        public IndexModel(Petean_David_Lab2.Data.Petean_David_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;

        public BookData BookD { get; set; } = new BookData();
        public int BookID { get; set; }
        public int CategoryID { get; set; }

        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            CurrentSort = sortOrder;

            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "Author" ? "author_desc" : "Author";

            if (searchString != null)
            {
                CurrentFilter = searchString;
            }

            var booksIQ = _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                    .ThenInclude(b => b.Category)
                .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                booksIQ = booksIQ.Where(s => s.Title.Contains(searchString)
                                       || s.Author.FullName.Contains(searchString)
                                       || s.Publisher.PublisherName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    booksIQ = booksIQ.OrderByDescending(s => s.Title);
                    break;
                case "Author":
                    booksIQ = booksIQ.OrderBy(s => s.Author.FullName);
                    break;
                case "author_desc":
                    booksIQ = booksIQ.OrderByDescending(s => s.Author.FullName);
                    break;
                default:
                    booksIQ = booksIQ.OrderBy(s => s.Title);
                    break;
            }

            BookD.Books = await booksIQ.ToListAsync();

            if (id != null)
            {
                BookID = id.Value;

                Book book = BookD.Books
                    .FirstOrDefault(i => i.ID == id.Value);

                if (book != null)
                {
                    BookD.Categories = book.BookCategories.Select(s => s.Category);

                    if (categoryID != null)
                    {
                        CategoryID = categoryID.Value;

                        BookD.Books = book.BookCategories
                            .Where(c => c.CategoryID == categoryID.Value)
                            .Select(c => c.Book)
                            .ToList();
                    }
                }
            }
        }
    }
}

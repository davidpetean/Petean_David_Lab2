using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Petean_David_Lab2.Data;
using Petean_David_Lab2.Models;

namespace Petean_David_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Petean_David_Lab2.Data.Petean_David_Lab2Context _context;

        public IndexModel(Petean_David_Lab2.Data.Petean_David_Lab2Context context)
        {
            _context = context;
        }

        public BookData BookD { get; set; } = new BookData();
        public int BookID { get; set; }
        public int CategoryID { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID)
        {
            BookD = new BookData();

            BookD.Books = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                    .ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            if (id != null)
            {
                BookID = id.Value;

                Book book = BookD.Books
                    .Where(i => i.ID == id.Value).Single();

                BookD.Categories = book.BookCategories.Select(s => s.Category);

                if (categoryID != null)
                {
                    CategoryID = categoryID.Value;
                    // Filtrarea cărților după categoria selectată
                    BookD.Books = book.BookCategories
                        .Where(c => c.CategoryID == categoryID.Value)
                        .Select(c => c.Book)
                        .ToList();
                }
            }
        }
    }
}

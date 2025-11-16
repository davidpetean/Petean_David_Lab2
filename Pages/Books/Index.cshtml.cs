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

        public IList<Book> Book { get;set; } = default!;
        public async Task OnGetAsync()
        {
            if (_context.Book != null)
            {
                Book = await _context.Book
                    // Include Author pentru a accesa numele complet
                    .Include(b => b.Author)
                    // Include Publisher (dacă ai și relația Publisher)
                    .Include(b => b.Publisher)
                    .ToListAsync();
            }
        }
    }
}

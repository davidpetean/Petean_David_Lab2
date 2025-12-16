using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Petean_David_Lab2.Data;
using Petean_David_Lab2.Models;
using Petean_David_Lab2.Models.ViewModels; // Spațiul de nume corect pentru structura Models/ViewModels

namespace Petean_David_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Petean_David_Lab2.Data.Petean_David_Lab2Context _context;

        public IndexModel(Petean_David_Lab2.Data.Petean_David_Lab2Context context)
        {
            _context = context;
        }

        public CategoryData CategoryD { get; set; } = new CategoryData();

        public int CategoryID { get; set; }

        public async Task OnGetAsync(int? id)
        {
            CategoryD.Categories = await _context.Category
                .Include(c => c.BookCategories)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.Author)
                .OrderBy(c => c.CategoryName)
                .AsNoTracking()
                .ToListAsync();

            if (id != null)
            {
                CategoryID = id.Value;

                Category category = CategoryD.Categories
                    .FirstOrDefault(i => i.ID == id.Value);

                if (category != null)
                {
                    CategoryD.Books = category.BookCategories.Select(bc => bc.Book);
                }
            }
        }
    }
}

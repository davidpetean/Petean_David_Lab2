using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Petean_David_Lab2.Data;
using Petean_David_Lab2.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Petean_David_Lab2.Pages.Books
{
    // Moștenire din clasa helper
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Petean_David_Lab2Context _context;

        public CreateModel(Petean_David_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        // 1. CORECȚIE: Proprietatea AssignedCategoryDataList pentru acces în Razor
        public List<AssignedCategoryData> AssignedCategoryDataList { get; set; } = new List<AssignedCategoryData>();

        public IActionResult OnGet()
        {
            // Populează dropdown-urile
            var authorList = _context.Author.Select(a => new
            {
                a.ID,
                FullName = a.FirstName + " " + a.LastName
            });

            ViewData["AuthorID"] = new SelectList(authorList, "ID", "FullName");
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");

            // 2. CORECȚIE: Atribuie lista returnată de metoda PopulateAssignedCategoryData
            AssignedCategoryDataList = PopulateAssignedCategoryData(_context, new Book { BookCategories = new List<BookCategory>() });

            return Page();
        }

        // Modificat OnPostAsync pentru a primi și lista de categorii selectate
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            // Verificăm ModelState înainte de a încerca să salvăm
            if (!ModelState.IsValid) // Simplificăm verificarea la doar ModelState.IsValid
            {
                // Repopulăm datele în caz de eroare de validare
                var authorList = _context.Author.Select(a => new
                {
                    a.ID,
                    FullName = a.FirstName + " " + a.LastName
                });
                ViewData["AuthorID"] = new SelectList(authorList, "ID", "FullName");
                ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");

                // Repopulăm și lista de categorii
                AssignedCategoryDataList = PopulateAssignedCategoryData(_context, Book);

                return Page();
            }

            // Inițiem o entitate nouă pentru a evita probleme de tracking (Recomandat pentru Create)
            var newBook = new Book();

            // Încercăm să actualizăm proprietățile simple (care vin din formular) în noul obiect
            if (await TryUpdateModelAsync<Book>(
                newBook,
                "Book",
                i => i.Title, i => i.AuthorID,
                i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                // Inițializăm colecția de legături înainte de a adăuga
                newBook.BookCategories = new List<BookCategory>();

                _context.Book.Add(newBook);
                await _context.SaveChangesAsync();

                // Salvează legăturile Many-to-Many
                UpdateBookCategories(_context, selectedCategories, newBook);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            // Dacă TryUpdateModelAsync eșuează, repopulăm
            AssignedCategoryDataList = PopulateAssignedCategoryData(_context, newBook);
            return Page();
        }
    }
}


using Microsoft.AspNetCore.Mvc.RazorPages;
using Petean_David_Lab2.Data;
using Petean_David_Lab2.Models;
using System.Collections.Generic;
using System.Linq;

namespace Petean_David_Lab2.Models
{
    public class BookCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData>
    PopulateAssignedCategoryData(Petean_David_Lab2Context context,
    Book book)
        {
            var allCategories = context.Category;
            var bookCategories = new HashSet<int>
                (
                book.BookCategories?.Select(c => c.CategoryID) ?? Enumerable.Empty<int>
                    ()
                    );

            var assignedCategoryDataList = new List<AssignedCategoryData>
                ();

            foreach (var cat in allCategories)
            {
                assignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = bookCategories.Contains(cat.ID)
                });
            }

            return assignedCategoryDataList;
        }

        public void UpdateBookCategories(Petean_David_Lab2Context context,
        string[] selectedCategories, Book bookToUpdate)
        {
            if (selectedCategories == null)
            {
                bookToUpdate.BookCategories = new List<BookCategory>
                    ();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>
                (selectedCategories);
            var bookCategories = new HashSet<int>
                (bookToUpdate.BookCategories.Select(c => c.CategoryID));

            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!bookCategories.Contains(cat.ID))
                    {
                        bookToUpdate.BookCategories.Add(
                        new BookCategory
                        {
                            BookID = bookToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (bookCategories.Contains(cat.ID))
                    {
                        BookCategory courseToRemove
                        = bookToUpdate
                        .BookCategories
                        .SingleOrDefault(i => i.CategoryID == cat.ID);

                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}


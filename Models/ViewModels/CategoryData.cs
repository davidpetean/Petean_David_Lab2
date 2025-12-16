using System.Collections.Generic;
using Petean_David_Lab2.Models;

namespace Petean_David_Lab2.Models.ViewModels
{
    public class CategoryData
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}

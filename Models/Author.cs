using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Petean_David_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        public ICollection<Book>? Books { get; set; }
    }
}

using System.Collections.Generic;

namespace Petean_David_Lab2.Models
{
    public class Publisher
    {
        public int ID { get; set; }
        public string PublisherName { get; set; } = string.Empty;

        // Relația cu Books (opțional dacă Publisher nu are cărți)
        public ICollection<Book>? Books { get; set; }
    }
}



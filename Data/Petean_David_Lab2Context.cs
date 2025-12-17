using Microsoft.EntityFrameworkCore;
using Petean_David_Lab2.Models;

namespace Petean_David_Lab2.Data
{
    public class Petean_David_Lab2Context : DbContext
    {
        public Petean_David_Lab2Context(DbContextOptions<Petean_David_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<Publisher> Publisher { get; set; } = default!;
        public DbSet<Author> Author { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<Member> Member { get; set; } = default!;
        public DbSet<Borrowing> Borrowing { get; set; } = default!;
    }
}



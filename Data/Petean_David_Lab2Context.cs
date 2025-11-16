using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Petean_David_Lab2.Models;

namespace Petean_David_Lab2.Data
{
    public class Petean_David_Lab2Context : DbContext
    {
        public Petean_David_Lab2Context (DbContextOptions<Petean_David_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Petean_David_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Petean_David_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Author> Author { get; set; } = default!;
    }
}

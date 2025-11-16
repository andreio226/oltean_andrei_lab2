using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using oltean_andrei_lab2.Models;

namespace oltean_andrei_lab2.Data
{
    public class oltean_andrei_lab2Context : DbContext
    {
        public oltean_andrei_lab2Context (DbContextOptions<oltean_andrei_lab2Context> options)
            : base(options)
        {
        }

        public DbSet<oltean_andrei_lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<oltean_andrei_lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<oltean_andrei_lab2.Models.Author> Author { get; set; } = default!;
        public DbSet<oltean_andrei_lab2.Models.Category> Category { get; set; } = default!;
        
        public DbSet<oltean_andrei_lab2.Models.Member> Member { get; set; } = default!;
        public DbSet<oltean_andrei_lab2.Models.Borrowing> Borrowing { get; set; } = default!;
    }
}
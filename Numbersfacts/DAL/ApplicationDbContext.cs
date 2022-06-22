using Microsoft.EntityFrameworkCore;
using Numbersfacts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numbersfacts.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fact>().Property(p => p.Id).ValueGeneratedOnAdd();
        }

        public DbSet<Fact> Fact { get; set; }
    }
}

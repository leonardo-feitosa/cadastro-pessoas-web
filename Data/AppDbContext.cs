using CadastroPessoasWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroPessoasWeb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
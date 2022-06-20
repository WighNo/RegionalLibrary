using Microsoft.EntityFrameworkCore;
using RegionalLibrary.Model;

namespace RegionalLibrary
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Database.db"); 
        }
        
        public DbSet<Author> Authors { get; private set; }
        public DbSet<Genre> Genres { get; private set; }
        
        public DbSet<Book> Books { get; private set; }
        public DbSet<Delivery> Deliveries { get; private set; }
        
        public DbSet<Employee> Employees { get; private set; }
        public DbSet<Visitor> Visitors { get; private set; }
    }
}
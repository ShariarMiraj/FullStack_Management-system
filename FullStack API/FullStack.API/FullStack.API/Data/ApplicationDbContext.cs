using FullStack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base (options)
     
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}

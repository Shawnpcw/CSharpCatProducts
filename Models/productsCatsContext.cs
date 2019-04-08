using Microsoft.EntityFrameworkCore;
 
namespace productsCats.Models
{
    public class productsCatsContext : DbContext
    {
        
        public productsCatsContext(DbContextOptions<productsCatsContext> options) : base(options) { }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Association> associations { get; set; }
        
    }
}

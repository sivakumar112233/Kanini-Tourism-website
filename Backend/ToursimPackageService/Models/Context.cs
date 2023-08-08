using Microsoft.EntityFrameworkCore;
using ToursimPackageService.Model;

namespace ToursimPackageService.Models
{
    public class Context:DbContext
    {
      public Context(DbContextOptions options ):base(options)
        {

        }
        public Context()
        {

        }
      

        public DbSet<Inclusion> ?Inclusion { get; set; }  

        public DbSet<TotalDaysDescription> ?TotalDaysDescriptions { get; set; }

        public DbSet<Tours> ?Tours { get; set; } 
      

      

    }
    
}

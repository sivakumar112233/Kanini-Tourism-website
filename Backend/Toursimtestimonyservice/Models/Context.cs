using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Toursimtestimonyservice.Models
{
    public class Context:DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<FeedBack>? FeedBacks { get; set; }
    }
}

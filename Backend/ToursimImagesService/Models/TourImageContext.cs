using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static ToursimImagesService.Models.TourImages; 

namespace ToursimImagesService.Models
{
    public class TourImageContext : DbContext
    {
        public TourImageContext(DbContextOptions<TourImageContext> options) : base(options)
        {
            Database.EnsureCreated();

        }
     
        public DbSet<TourImages> TourPackageImagesContainer { get; set; }


    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class MyDbContext: IdentityDbContext //<IdentityUser>   // DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {
                
        }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Bike>   Bikes { get; set; }
         
    }
}

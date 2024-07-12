using _30321_Barkovskaya_API.Data;
using _30321_BarkovskayaDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;


namespace _30321_Barkovskaya_API.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public AppDbContext()
        {
        }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
    

}

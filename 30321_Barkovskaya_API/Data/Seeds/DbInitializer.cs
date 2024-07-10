using _30321_BarkovskayaDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _30321_Barkovskaya_API.Data.Seeds
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            // Uri проекта
            var uri = "https://localhost:7002/";
            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();

            // Заполнение данными
            if (!context.Categories.Any() && !context.Dishes.Any())
            {
                var brands = new Category[]
                {
                    new Category { Name="Стартеры", NormalizedName="starters" },
                    new Category { Name="Салаты", NormalizedName="salads" },
                    new Category { Name="Супы", NormalizedName="soups" },
                    new Category { Name="Основное", NormalizedName="main" },
                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
                var dish = new List<Dish>
                {
                    new Dish
                    {
                        Name = "Канапе",
                        Description = "Закуска из томатов и сыра",
                        Price = 50,
                        Image = uri + "images/канапе.jpg",
                        CategoryId = 1,
                        Category=categories.FirstOrDefault(b => b.NormalizedName.Equals("starters")),
                    },
                    new Dish
                    {
                        Name = "Цезарь",
                        Description = "Овощи, сыр, яйцо, сухарики",
                        Price = 25,
                        Image =uri + "images/цезарь.jpg",
                        CategoryId = 2,
                        Category=categories.FirstOrDefault(b => b.NormalizedName.Equals("salads")),
                    },
                    new Dish
                    {
                        Name = "Суп-харчо",
                        Description = "Очень острый, мясной",
                        Price = 17,
                        Image = uri + "images/харчо.jpg",
                        CategoryId = 3,
                        Category=categories.FirstOrDefault(b => b.NormalizedName.Equals("soups")),
                    },
                    new Dish
                    {
                        Name = "Борщ",
                        Description = "Свекла, капуста, мясо, без сметаны",
                        Price = 15,
                        Image = uri + "images/борщ.jpg",
                        CategoryId = 3,
                        Category=categories.FirstOrDefault(b => b.NormalizedName.Equals("soups")),
                    },
                    new Dish
                    {
                        Name = "Томагавк",
                        Description = "Мясо на кости, овощи",
                        Price = 55,
                        Image = uri + "images/томагавк.jpg",
                        CategoryId = 4,
                        Category=categories.FirstOrDefault(b => b.NormalizedName.Equals("main")),
                    },
                };
                await context.AddRangeAsync(dishes);
                await context.SaveChangesAsync();
            }
        }
    }
}

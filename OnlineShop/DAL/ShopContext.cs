using Lista10.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Lista10.DAL
{
    public class ShopContext : IdentityDbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }


        public Microsoft.EntityFrameworkCore.DbSet<Category> Categories { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Article> Articles { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Cart> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }


    }

    public static class ModelBuilderExtension
    {

        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    CategoryID = 1,
                    Name = "Make-up",
                },
                new Category()
                {
                    CategoryID = 2,
                    Name = "Clothes"
                },
                new Category()
                {
                    CategoryID = 3,
                    Name = "Flowers"
                }); ;

            modelBuilder.Entity<Article>().HasData(
               new Article()
               {
                   ArticleID = 1,
                   Name = "Black Jeans",
                   Price = 99,
                   CategoryID = 2
               },
               new Article()
               {
                   ArticleID = 2,
                   Name = "Red Lipstick",
                   Price = 20,
                   CategoryID = 1
               },
               new Article()
               {
                   ArticleID = 3,
                   Name = "Rose",
                   Price = 12,
                   CategoryID = 3
               },
               new Article()
               {
                   ArticleID = 4,
                   Name = "Baseball Cap",
                   Price = 23,
                   CategoryID = 2
               }
               ); ;
        }

    }


}


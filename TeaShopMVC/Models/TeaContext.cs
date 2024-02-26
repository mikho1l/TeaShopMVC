using Microsoft.EntityFrameworkCore;

namespace TeaShopMVC.Models
{
    public class TeaContext : DbContext
    {
        public DbSet<Tea> Tea { get; set; }
        public DbSet<TeaType> TeaType { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<CartItem> ShoppingCarts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public TeaContext(DbContextOptions<TeaContext> options)
             : base(options)
        {

             //Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        public DbSet<Review> Reviews { get; set; }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp
{
    public class TodoDbContext : IdentityDbContext
    {
        public DbSet<ItemData> ItemDatas { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        
        public TodoDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}

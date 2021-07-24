using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApp.IRepository;
using TodoApp.Models;

namespace TodoApp.Repository
{
    public class RestaurantRepository :IRestaurantRepository
    {
        private readonly TodoDbContext _dbContext;
        public RestaurantRepository(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Item>> GetAllItem()
        {
            return await _dbContext.Items.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrder()
        {
            return await _dbContext.Orders.Include("Customer").Include("OrderItems").ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomer()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItem()
        {
            return await _dbContext.OrderItems.ToListAsync();
        }

        public  async Task<bool> AddOrEditOrder(Order order)
        {
            //await _dbContext.OrderItems.AddRangeAsync(order.OrderItems);
            await _dbContext.Orders.AddAsync(order);
            await  _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
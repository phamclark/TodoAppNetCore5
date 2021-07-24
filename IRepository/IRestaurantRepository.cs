using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.IRepository
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Item>> GetAllItem();
        Task<IEnumerable<Order>> GetAllOrder();
        Task<IEnumerable<Customer>> GetAllCustomer();
        Task<IEnumerable<OrderItem>> GetAllOrderItem();
        Task<bool> AddOrEditOrder(Order order);
    }
}
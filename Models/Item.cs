using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TodoApp.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
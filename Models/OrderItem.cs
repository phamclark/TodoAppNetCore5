using System;
using System.Text.Json.Serialization;

namespace TodoApp.Models
{
    public class OrderItem
    {
        public long OrderItemId { get; set; }
        public int Quantity { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
        public DateTime CreatedDate { get; set; }  
    }
}
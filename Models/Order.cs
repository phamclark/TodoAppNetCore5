using System;
using System.Collections.Generic;

namespace TodoApp.Models
{
    public class Order
    {
        public long OrderId  { get; set; }
        public string OrderNo { get; set; }
        public string PMethod { get; set; }
        public decimal? GTotal { get; set; }
        public string DeletedOrderItemIds { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
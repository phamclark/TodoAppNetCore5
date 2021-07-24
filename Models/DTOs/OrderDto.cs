using System.Collections.Generic;

namespace TodoApp.Models.DTOs
{
    public class OrderDto
    {
        public long OrderId  { get; set; }
        public string OrderNo { get; set; }
        public string PMethod { get; set; }
        public decimal? GTotal { get; set; }
        public string DeletedOrderItemIds { get; set; }

        public int CustomerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public  class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
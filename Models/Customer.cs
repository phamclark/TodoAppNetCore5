using System.Collections.Generic;
using Newtonsoft.Json;

namespace TodoApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Order> Orders  { get; set; }
    }
}
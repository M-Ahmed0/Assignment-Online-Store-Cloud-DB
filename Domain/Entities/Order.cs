using Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : IEntityBase
    {
        public int Id { get; set; }
        public string PartitionKey { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }

        public OrderStatus Status { get; set; }

        public ShippingAddress ShippingAddress { get; set; }

        // User relationship
        public User User { get; set; }

        // Product relationship
        public List<Product> Products { get; set; }

    }
}

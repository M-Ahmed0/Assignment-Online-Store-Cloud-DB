using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class OrderDTO
    {
        public string Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public OrderStatus Status { get; set; }
        public ShippingAddress ShippingAddress { get; set; }

        // More descriptive user-related data. This could be username, email, etc.
        public string UserName { get; set; }

        // List of products associated with the order.
        public List<ProductDTO> Products { get; set; }
    }

}

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class OrderCreationDTO
    {
        public DateTime? ShippingDate { get; set; }
        public OrderStatus Status { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public string UserId { get; set; }
        public Dictionary<string, int> ProductQuantities { get; set; }
    }

}

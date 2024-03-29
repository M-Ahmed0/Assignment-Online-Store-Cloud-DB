﻿using Domain.Entities;
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
        public string PartitionKey { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public OrderStatus Status { get; set; }
        public ShippingAddress ShippingAddress { get; set; }

 
        public string UserId { get; set; }

        // List of products associated with the order.
        public List<string> ProductIds { get; set; } = new List<string>();
    }

}

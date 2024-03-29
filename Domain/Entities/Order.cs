﻿using Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : EntityBase
    {
        public string Id { get; set; }
        public string PartitionKey { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }

        public OrderStatus Status { get; set; }

        public ShippingAddress ShippingAddress { get; set; }

        // Reference to User
        public string UserId { get; set; }

        // References to Products
        public List<string> ProductIds { get; set; } = new List<string>();

    }
}

﻿using Domain.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review : EntityBase
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public string Name { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public double Rating { get; set; }

        public DateTime CreatedAt { get; set; }

        // Product relationship
        public string ProductId { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool InStock { get; set; } // Indicates whether the product is in stock or not.
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}

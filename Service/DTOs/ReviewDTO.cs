using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class ReviewDTO
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProductId { get; set; }
    }
}

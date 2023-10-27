using Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review : IEntityBase
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Name { get; set; } // This could be the name of the person reviewing.
        public double Rating { get; set; }
        public DateTime CreatedAt { get; set; }

        // Product relationship
        public int ProductId { get; set; }

    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository : EntityBaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, int>> GetProductStocks(List<string> productIds)
        {
            return await _context.Products
                                 .Where(p => productIds.Contains(p.Id))
                                 .ToDictionaryAsync(p => p.Id, p => p.Stock);
        }


        
    }

}

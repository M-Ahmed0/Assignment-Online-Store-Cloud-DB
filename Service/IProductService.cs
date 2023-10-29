using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IProductService
    {
        Task<Dictionary<string, int>> GetProductStocks(List<string> productIds);
        Task<Product> GetProductById(string id);

        
        Task<Product> AddProduct(Product product);

    }
}

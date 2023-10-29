using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Dictionary<string, int>> GetProductStocks(List<string> productIds);
       
    }

}

using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Dictionary<string, int>> GetProductStocks(List<string> productIds)
        {
            return await _productRepository.GetProductStocks(productIds);
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _productRepository.GetSingle(id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _productRepository.Add(product);
            return product;
        }
    }

    }

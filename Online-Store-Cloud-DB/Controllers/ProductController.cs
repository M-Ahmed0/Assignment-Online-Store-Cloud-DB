using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service;
using Service.DTOs;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Domain.Entities;

namespace Online_Store_Cloud_DB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper; 

        public ProductController(IProductService productService, IConfiguration configuration, IMapper mapper) 
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _configuration = configuration;
            _mapper = mapper;  
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromForm] ProductDTO productDto)
        {
            if (productDto == null || productDto.ImageFile == null)  // Check ImageFile
            {
                return BadRequest("Invalid product data or missing image.");
            }

            try
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(productDto.ImageFile.FileName);
                BlobServiceClient blobServiceClient = new BlobServiceClient(_configuration.GetConnectionString("AzureWebJobsStorage"));

                if (blobServiceClient == null)
                {
                    throw new InvalidOperationException("BlobServiceClient is not initialized.");
                }

                string containerName = _configuration["ContainerName"];

                if (string.IsNullOrEmpty(containerName))
                {
                    throw new InvalidOperationException("Container name is not provided in the configuration.");
                }

                BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

                if (!blobContainerClient.Exists())
                {
                    await blobServiceClient.CreateBlobContainerAsync(containerName);
                }

                var blob = blobContainerClient.GetBlobClient(filename);

                using (var stream = productDto.ImageFile.OpenReadStream())
                {
                    await blob.UploadAsync(stream);
                }

                productDto.Filename = filename;  // Update the filename property with the generated filename

                // Use AutoMapper to convert ProductDTO to Product
                Product product = _mapper.Map<Product>(productDto);

                // Now pass the Product entity to your service method
                var createdProduct = await _productService.AddProduct(product);

                // Assuming you want to return a DTO, map the created Product back to a ProductDTO
                var createdProductDto = _mapper.Map<ProductDTO>(createdProduct);

                return CreatedAtAction(nameof(_productService.GetProductById), new { id = createdProductDto.Id }, createdProductDto);
            }
            catch (Exception ex)
            {
                // Log the exception here (using a logger, if available).
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}

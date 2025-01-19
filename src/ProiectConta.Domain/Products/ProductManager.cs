using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace ProiectConta.Products
{
    public class ProductManager : DomainService
    {
        private readonly IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> CreateAsync(
            string name,
            float price)
        {
            var existingProduct = await _productRepository.FindByNameAsync(name);
            if (existingProduct != null)
            {
                throw new ProductAlreadyExistsException(name);
            }
            return new Product(
                GuidGenerator.Create(),
                name,
                price
            );
        }
        public async Task ChangeNameAsync(Product product, string name)
        {
            var existingProduct = await _productRepository.FindByNameAsync(name);
            if (existingProduct != null && existingProduct.Id != product.Id)
            {
                throw new ProductAlreadyExistsException(name);
            }
            product.ChangeName(name);
        }
    }
}

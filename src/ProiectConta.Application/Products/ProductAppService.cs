using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProiectConta.Products
{
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductManager _productManager;

        public ProductAppService(
            IProductRepository productRepository,
            ProductManager productManager)
        {
            _productRepository = productRepository;
            _productManager = productManager;
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        public async Task<ProductDto> GetProductAsync(string name)
        {
            var product = await _productRepository.FindByNameAsync(name);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        public async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            var product = await _productManager.CreateAsync(
                input.Name,
                input.Price ?? 0
            );

            await _productRepository.InsertAsync(product, autoSave: true);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        public async Task UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            var product = await _productRepository.GetAsync(id);

            if (product.Name != input.Name)
            {
                await _productManager.ChangeNameAsync(product, input.Name);
            }

            product.Price = input.Price ?? product.Price;

            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Product.Name);
            }

            var products = await _productRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = await _productRepository.GetCountAsync();

            return new PagedResultDto<ProductDto>(
                totalCount,
                ObjectMapper.Map<List<Product>, List<ProductDto>>(products)
            );
        }

    }
}

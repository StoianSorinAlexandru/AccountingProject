using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectConta.Products
{
    public interface IProductAppService
    {
        Task<ProductDto> GetAsync(Guid id);
        Task<List<ProductDto>> GetListAsync();
        Task<ProductDto> CreateAsync(CreateUpdateProductDto input);
        Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input);
        Task DeleteAsync(Guid id);
    }
}

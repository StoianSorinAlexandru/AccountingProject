using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProiectConta.Products
{
    public interface IProductAppService
    {
        Task<ProductDto> GetAsync(Guid id);
        Task<List<ProductDto>> GetAllAsync();
        Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input);
        Task<ProductDto> CreateAsync(CreateUpdateProductDto input);
        Task UpdateAsync(Guid id, CreateUpdateProductDto input);
        Task DeleteAsync(Guid id);
    }
}

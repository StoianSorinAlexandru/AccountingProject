using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProiectConta.Gestions
{
    public interface IGestionAppService
    {
        Task<GestionDto> GetAsync(Guid id);
        Task<List<GestionDto>> GetAllAsync();
        Task<PagedResultDto<GestionDto>> GetListAsync(GetGestionListDto input);
        Task<GestionDto> CreateAsync(CreateUpdateGestionDto input);
        Task UpdateAsync(Guid id, CreateUpdateGestionDto input);
        Task DeleteAsync(Guid id);
    }
}

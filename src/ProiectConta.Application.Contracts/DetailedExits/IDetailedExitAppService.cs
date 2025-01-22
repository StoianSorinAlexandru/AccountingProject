using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProiectConta.DetailedExits
{
    public interface IDetailedExitAppService
    {
        Task<DetailedExitDto> GetAsync(Guid id);
        Task<PagedResultDto<DetailedExitDto>> GetListAsync(GetDetailedExitListDto input);
        Task<DetailedExitDto> CreateAsync(CreateUpdateDetailedExitDto input);
        Task UpdateAsync(Guid id, CreateUpdateDetailedExitDto input);
        Task DeleteAsync(Guid id);
        Task<DetailedExitDto> FindByExitId(Guid id);
    }
}

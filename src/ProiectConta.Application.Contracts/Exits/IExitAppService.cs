using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProiectConta.Exits
{
    public interface IExitAppService
    {
        Task<ExitDto> GetAsync(Guid id);
        Task<PagedResultDto<ExitDto>> GetListAsync(GetExitListDto input);
        Task<ExitDto> CreateAsync(CreateUpdateExitDto input);
        Task UpdateAsync(Guid id, CreateUpdateExitDto input);
        Task DeleteAsync(Guid id);
    }
}

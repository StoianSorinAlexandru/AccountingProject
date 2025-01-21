using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProiectConta.DetailedEntries
{
    public interface IDetailedEntryAppService
    {
        Task<DetailedEntryDto> GetAsync(Guid id);
        Task<PagedResultDto<DetailedEntryDto>> GetListAsync(GetDetailedEntryListDto input);
        Task<DetailedEntryDto> CreateAsync(CreateUpdateDetailedEntryDto input);
        Task UpdateAsync(Guid id, CreateUpdateDetailedEntryDto input);
        Task DeleteAsync(Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProiectConta.Entries
{
    public interface IEntryAppService
    {
        Task<EntryDto> GetAsync(Guid id);
        Task<PagedResultDto<EntryDto>> GetListAsync(GetEntryListDto input);
        Task<EntryDto> CreateAsync(CreateUpdateEntryDto input);
        Task UpdateAsync(Guid id, CreateUpdateEntryDto input);
        Task DeleteAsync(Guid id);
    }
}

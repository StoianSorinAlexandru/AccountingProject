using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectConta.DetailedEntries
{
    public interface IDetailedEntryAppService
    {
        Task<DetailedEntryDto> GetAsync(Guid id);
        Task<List<DetailedEntryDto>> GetListAsync();
        Task<DetailedEntryDto> CreateAsync(CreateUpdateDetailedEntryDto input);
        Task<DetailedEntryDto> UpdateAsync(Guid id, CreateUpdateDetailedEntryDto input);
        Task DeleteAsync(Guid id);
    }
}

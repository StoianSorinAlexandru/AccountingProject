using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectConta.Entries
{
    public interface IEntryAppService
    {
        Task<EntryDto> GetAsync(Guid id);
        Task<List<EntryDto>> GetListAsync();
        Task<EntryDto> CreateAsync(CreateUpdateEntryDto input);
        Task<EntryDto> UpdateAsync(Guid id, CreateUpdateEntryDto input);
        Task DeleteAsync(Guid id);
    }
}

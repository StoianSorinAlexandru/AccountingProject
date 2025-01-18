using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectConta.DetailedExits
{
    public interface IDetailedExitAppService
    {
        Task<DetailedExitDto> GetAsync(Guid id);
        Task<List<DetailedExitDto>> GetListAsync();
        Task<DetailedExitDto> CreateAsync(CreateUpdateDetailedExitDto input);
        Task<DetailedExitDto> UpdateAsync(Guid id, CreateUpdateDetailedExitDto input);
        Task DeleteAsync(Guid id);
    }
}

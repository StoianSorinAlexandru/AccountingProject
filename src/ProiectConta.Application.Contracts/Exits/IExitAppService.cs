using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectConta.Exits
{
    public interface IExitAppService
    {
        Task<ExitDto> GetAsync(Guid id);
        Task<List<ExitDto>> GetListAsync();
        Task<ExitDto> CreateAsync(CreateUpdateExitDto input);
        Task<ExitDto> UpdateAsync(Guid id, CreateUpdateExitDto input);
        Task DeleteAsync(Guid id);
    }
}

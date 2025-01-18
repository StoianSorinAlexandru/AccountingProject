using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectConta.Gestions
{
    public interface IGestionAppService
    {
        Task<GestionDto> GetAsync(Guid id);
        Task<List<GestionDto>> GetListAsync();
        Task<GestionDto> CreateAsync(CreateUpdateGestionDto input);
        Task<GestionDto> UpdateAsync(Guid id, CreateUpdateGestionDto input);
        Task DeleteAsync(Guid id);
    }
}

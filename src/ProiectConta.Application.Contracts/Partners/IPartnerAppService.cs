using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectConta.Partners
{
    public interface IPartnerService
    {
        Task<PartnerDto> GetAsync(Guid id);
        Task<List<PartnerDto>> GetListAsync();
        Task<PartnerDto> CreateAsync(CreateUpdatePartnerDto input);
        Task<PartnerDto> UpdateAsync(Guid id, CreateUpdatePartnerDto input);
        Task DeleteAsync(Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProiectConta.Partners
{
    public interface IPartnerAppService
    {
        Task<PartnerDto> GetAsync(Guid id);
        Task<List<PartnerDto>> GetAllAsync();
        Task<PagedResultDto<PartnerDto>> GetListAsync(GetPartnerListDto input);
        Task<PartnerDto> CreateAsync(CreateUpdatePartnerDto input);
        Task UpdateAsync(Guid id, CreateUpdatePartnerDto input);
        Task DeleteAsync(Guid id);
    }
}

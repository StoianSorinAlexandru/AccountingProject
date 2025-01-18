using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ProiectConta.Partners
{
    public class PartnerService : ApplicationService, IPartnerService
    {
        private readonly IPartnerRepository _partnerRepository;

        public PartnerService(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }

        public async Task<PartnerDto> GetAsync(Guid id)
        {
            var partner = await _partnerRepository.GetAsync(id);
            return ObjectMapper.Map<Partner, PartnerDto>(partner);
        }

        public async Task<List<PartnerDto>> GetListAsync()
        {
            var partners = await _partnerRepository.GetListAsync();
            return ObjectMapper.Map<List<Partner>, List<PartnerDto>>(partners);
        }

        public async Task<PartnerDto> CreateAsync(CreateUpdatePartnerDto input)
        {
            var partner = ObjectMapper.Map<CreateUpdatePartnerDto, Partner>(input);
            var createdPartner = await _partnerRepository.InsertAsync(partner);
            return ObjectMapper.Map<Partner, PartnerDto>(createdPartner);
        }

        public async Task<PartnerDto> UpdateAsync(Guid id, CreateUpdatePartnerDto input)
        {
            var partner = await _partnerRepository.GetAsync(id);
            ObjectMapper.Map(input, partner);
            var updatedPartner = await _partnerRepository.UpdateAsync(partner);
            return ObjectMapper.Map<Partner, PartnerDto>(updatedPartner);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _partnerRepository.DeleteAsync(id);
        }
    }
}

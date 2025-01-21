using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProiectConta.Partners
{
    public class PartnerAppService : ApplicationService, IPartnerAppService
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly PartnerManager _partnerManager;

        public PartnerAppService(IPartnerRepository partnerRepository, PartnerManager partnerManager)
        {
            _partnerRepository = partnerRepository;
            _partnerManager = partnerManager;
        }

        public async Task<PartnerDto> GetAsync(Guid id)
        {
            var partner = await _partnerRepository.GetAsync(id);
            return ObjectMapper.Map<Partner, PartnerDto>(partner);
        }

        public async Task<List<PartnerDto>> GetAllAsync()
        {
            var partners = await _partnerRepository.GetListAsync();
            return partners.Select(partner => ObjectMapper.Map<Partner, PartnerDto>(partner)).ToList();
        }

        public async Task<PartnerDto> GetPartnerAsync(string name)
        {
            var partner = await _partnerRepository.FindByNameAsync(name);
            return ObjectMapper.Map<Partner, PartnerDto>(partner);
        }

        public async Task<PartnerDto> CreateAsync(CreateUpdatePartnerDto input)
        {
            var partner = await _partnerManager.CreateAsync(
                input.Name,
                input.CUI,
                input.Address,
                input.Type
            );

            using(var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _partnerRepository.InsertAsync(partner);
                transaction.Complete();
            }
            return ObjectMapper.Map<Partner, PartnerDto>(partner);
        }

        public async Task UpdateAsync(Guid id, CreateUpdatePartnerDto input)
        {
            var partner = await _partnerRepository.GetAsync(id);
            if (partner.Name != input.Name)
            {
                await _partnerManager.ChangeNameAsync(partner, input.Name);
            }
            partner.CUI = input.CUI;
            partner.Address = input.Address;
            partner.Type = input.Type;
            await _partnerRepository.UpdateAsync(partner);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _partnerRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<PartnerDto>> GetListAsync(GetPartnerListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Partner.Name);
            }
            var partners = await _partnerRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = await _partnerRepository.GetCountAsync();

            var partnerListDto = partners.Select(partner => new PartnerDto
            {
                Id = partner.Id,
                Name = partner.Name,
                CUI = partner.CUI,
                Address = partner.Address,
                Type = partner.Type
            }).ToList();

            return new PagedResultDto<PartnerDto>(
                totalCount,
                partnerListDto
            );
        }

    }
}

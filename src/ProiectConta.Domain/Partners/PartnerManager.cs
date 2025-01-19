using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace ProiectConta.Partners
{
    public class PartnerManager : DomainService
    {
        private readonly IPartnerRepository _partnerRepository;
        public PartnerManager(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }
        public async Task<Partner> CreateAsync(
            string name,
            string cui,
            string address,
            PartnerType type)
        {
            var existingPartner = await _partnerRepository.FindByNameAsync(name);
            if (existingPartner != null)
            {
                throw new PartnerAlreadyExistsException(name);
            }
            return new Partner(
                GuidGenerator.Create(),
                name,
                cui,
                address,
                type
            );
        }

        public async Task ChangeNameAsync(Partner partner, string name)
        {
            var existingPartner = await _partnerRepository.FindByNameAsync(name);
            if (existingPartner != null && existingPartner.Id != partner.Id)
            {
                throw new PartnerAlreadyExistsException(name);
            }
            partner.ChangeName(name);
        }
    }
}

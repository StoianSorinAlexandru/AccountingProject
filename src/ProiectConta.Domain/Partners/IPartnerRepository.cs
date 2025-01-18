using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;

namespace ProiectConta.Partners
{
    public interface IPartnerRepository : IRepository<Partner, Guid>
    {
        Task<List<Partner>> GetPartnersByTypeAsync(PartnerType type);
    }
}

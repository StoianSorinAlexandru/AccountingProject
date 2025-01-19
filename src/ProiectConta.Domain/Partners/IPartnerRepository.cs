using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;

namespace ProiectConta.Partners
{
    public interface IPartnerRepository : IRepository<Partner, Guid>
    {
        Task<List<Partner>> GetPartnersByTypeAsync(PartnerType type);

        Task<Partner> FindByNameAsync(string name);

        Task<List<Partner>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
            );
    }
}

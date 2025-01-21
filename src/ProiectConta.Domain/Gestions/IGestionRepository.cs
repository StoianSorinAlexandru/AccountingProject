using Volo.Abp.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProiectConta.Gestions
{
    public interface IGestionRepository : IRepository<Gestion, Guid>
    {
        Task<Gestion> FindByNameAsync(string name);

        Task<List<Gestion>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );

        Task<Gestion> GetAsync(Guid id);


    }
}

using Volo.Abp.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProiectConta.Products
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<Product> FindByNameAsync(string name);

        Task<List<Product>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}

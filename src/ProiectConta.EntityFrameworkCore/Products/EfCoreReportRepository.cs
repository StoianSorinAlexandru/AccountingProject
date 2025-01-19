using Microsoft.EntityFrameworkCore;
using ProiectConta.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ProiectConta.Products
{
    public class EfCoreProductRepository : EfCoreRepository<ProiectContaDbContext, Product, Guid>, IProductRepository
    {
        public EfCoreProductRepository(IDbContextProvider<ProiectContaDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Product> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(product => product.Name == name);
        }

        public async Task<List<Product>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    product => product.Name.Contains(filter)
                )
                //.OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}

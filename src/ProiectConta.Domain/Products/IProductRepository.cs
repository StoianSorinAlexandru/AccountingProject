using Volo.Abp.Domain.Repositories;
using System;

namespace ProiectConta.Products
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
    }
}

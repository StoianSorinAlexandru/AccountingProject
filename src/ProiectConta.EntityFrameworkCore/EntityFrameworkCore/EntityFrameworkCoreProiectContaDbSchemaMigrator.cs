using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProiectConta.Data;
using Volo.Abp.DependencyInjection;

namespace ProiectConta.EntityFrameworkCore;

public class EntityFrameworkCoreProiectContaDbSchemaMigrator
    : IProiectContaDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreProiectContaDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the ProiectContaDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ProiectContaDbContext>()
            .Database
            .MigrateAsync();
    }
}

using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ProiectConta.Data;

/* This is used if database provider does't define
 * IProiectContaDbSchemaMigrator implementation.
 */
public class NullProiectContaDbSchemaMigrator : IProiectContaDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

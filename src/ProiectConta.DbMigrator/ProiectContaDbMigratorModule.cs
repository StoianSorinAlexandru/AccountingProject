using ProiectConta.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace ProiectConta.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ProiectContaEntityFrameworkCoreModule),
    typeof(ProiectContaApplicationContractsModule)
    )]
public class ProiectContaDbMigratorModule : AbpModule
{
}

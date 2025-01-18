using Volo.Abp.Modularity;

namespace ProiectConta;

/* Inherit from this class for your domain layer tests. */
public abstract class ProiectContaDomainTestBase<TStartupModule> : ProiectContaTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

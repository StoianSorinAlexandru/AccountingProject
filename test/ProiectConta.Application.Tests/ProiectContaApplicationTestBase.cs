using Volo.Abp.Modularity;

namespace ProiectConta;

public abstract class ProiectContaApplicationTestBase<TStartupModule> : ProiectContaTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

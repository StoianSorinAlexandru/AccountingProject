using Volo.Abp.Modularity;

namespace ProiectConta;

[DependsOn(
    typeof(ProiectContaDomainModule),
    typeof(ProiectContaTestBaseModule)
)]
public class ProiectContaDomainTestModule : AbpModule
{

}

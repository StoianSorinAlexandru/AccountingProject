using Volo.Abp.Modularity;

namespace ProiectConta;

[DependsOn(
    typeof(ProiectContaApplicationModule),
    typeof(ProiectContaDomainTestModule)
)]
public class ProiectContaApplicationTestModule : AbpModule
{

}

using ProiectConta.Localization;
using Volo.Abp.AspNetCore.Components;

namespace ProiectConta.Blazor;

public abstract class ProiectContaComponentBase : AbpComponentBase
{
    protected ProiectContaComponentBase()
    {
        LocalizationResource = typeof(ProiectContaResource);
    }
}

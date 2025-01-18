using Microsoft.Extensions.Localization;
using ProiectConta.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ProiectConta.Blazor;

[Dependency(ReplaceServices = true)]
public class ProiectContaBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ProiectContaResource> _localizer;

    public ProiectContaBrandingProvider(IStringLocalizer<ProiectContaResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}

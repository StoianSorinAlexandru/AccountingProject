using ProiectConta.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ProiectConta.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ProiectContaController : AbpControllerBase
{
    protected ProiectContaController()
    {
        LocalizationResource = typeof(ProiectContaResource);
    }
}

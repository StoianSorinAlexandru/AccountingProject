using System;
using System.Collections.Generic;
using System.Text;
using ProiectConta.Localization;
using Volo.Abp.Application.Services;

namespace ProiectConta;

/* Inherit your application services from this class.
 */
public abstract class ProiectContaAppService : ApplicationService
{
    protected ProiectContaAppService()
    {
        LocalizationResource = typeof(ProiectContaResource);
    }
}

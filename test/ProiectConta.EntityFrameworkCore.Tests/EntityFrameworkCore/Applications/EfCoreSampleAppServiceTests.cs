using ProiectConta.Samples;
using Xunit;

namespace ProiectConta.EntityFrameworkCore.Applications;

[Collection(ProiectContaTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ProiectContaEntityFrameworkCoreTestModule>
{

}

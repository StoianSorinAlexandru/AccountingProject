using ProiectConta.Samples;
using Xunit;

namespace ProiectConta.EntityFrameworkCore.Domains;

[Collection(ProiectContaTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ProiectContaEntityFrameworkCoreTestModule>
{

}

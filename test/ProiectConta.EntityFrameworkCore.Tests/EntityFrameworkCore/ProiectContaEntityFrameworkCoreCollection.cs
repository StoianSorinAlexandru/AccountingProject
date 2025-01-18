using Xunit;

namespace ProiectConta.EntityFrameworkCore;

[CollectionDefinition(ProiectContaTestConsts.CollectionDefinitionName)]
public class ProiectContaEntityFrameworkCoreCollection : ICollectionFixture<ProiectContaEntityFrameworkCoreFixture>
{

}

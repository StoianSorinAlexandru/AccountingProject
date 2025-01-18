using System.Threading.Tasks;

namespace ProiectConta.Data;

public interface IProiectContaDbSchemaMigrator
{
    Task MigrateAsync();
}

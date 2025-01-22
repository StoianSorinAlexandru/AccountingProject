using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace ProiectConta.Exits
{
    public class ExitManager : DomainService
    {
        private readonly IExitRepository _exitRepository;

        public ExitManager(IExitRepository exitRepository)
        {
            _exitRepository = exitRepository;
        }

        public async Task<Exit> CreateAsync(
            DateTime date,
            Guid partnerId,
            Guid gestionId)
        {
            return new Exit(
                GuidGenerator.Create(),
                date,
                partnerId,
                gestionId
            );
        }

        public async Task<Exit> CreateUnrelatedAsync(
            DateTime date)
        {
            return new Exit(
                GuidGenerator.Create(),
                date
                );
        }

        public async Task ChangeDateAsync(Exit exit, DateTime date)
        {
            exit.Date = date;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace ProiectConta.DetailedExits
{
    public class DetailedExitManager : DomainService
    {
        private readonly IDetailedExitRepository _detailedExitRepository;
        public DetailedExitManager(IDetailedExitRepository detailedExitRepository)
        {
            _detailedExitRepository = detailedExitRepository;
        }
        public async Task<DetailedExit> CreateAsync(
            Guid exitId,
            Guid productId,
            int quantity,
            float value)
        {
            return new DetailedExit(
                GuidGenerator.Create(),
                exitId,
                productId,
                quantity,
                value
            );
        }
    }
}

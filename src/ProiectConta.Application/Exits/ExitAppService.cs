using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProiectConta.Exits
{
    public class ExitAppService : ApplicationService, IExitAppService
    {
        private readonly IExitRepository _exitRepository;
        private readonly ExitManager _exitManager;

        public ExitAppService(IExitRepository exitRepository, ExitManager exitManager)
        {
            _exitRepository = exitRepository;
            _exitManager = exitManager;
        }

        public async Task<ExitDto> GetAsync(Guid id)
        {
            var exit = await _exitRepository.GetAsync(id);
            return ObjectMapper.Map<Exit, ExitDto>(exit);
        }

        public async Task<ExitDto> CreateAsync(CreateUpdateExitDto input)
        {
            var exit = await _exitManager.CreateAsync(
                input.Date,
                input.PartnerId,
                input.GestionId
            );
            using(var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _exitRepository.InsertAsync(exit, autoSave: true);
                transaction.Complete();
            }
            await _exitRepository.InsertAsync(exit, autoSave: true);
            return ObjectMapper.Map<Exit, ExitDto>(exit);
        }

        public async Task UpdateAsync(Guid id, CreateUpdateExitDto input)
        {
            var exit = await _exitRepository.GetAsync(id);
            if (exit.Date != input.Date)
            {
                await _exitManager.ChangeDateAsync(exit, input.Date);
            }
            exit.PartnerId = input.PartnerId;
            exit.GestionId = input.GestionId;
            await _exitRepository.UpdateAsync(exit);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _exitRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<ExitDto>> GetListAsync(GetExitListDto input)
        {
            var exits = await _exitRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );
            var totalCount = await _exitRepository.GetCountAsync();

            var exitDtoList = exits.Select(exit => new ExitDto
            {
                Id = exit.Id,
                Date = exit.Date,
                PartnerId = exit.PartnerId,
                GestionId = exit.GestionId
            }).ToList();

            return new PagedResultDto<ExitDto>(
                totalCount,
                exitDtoList
            );
        }


    }
}

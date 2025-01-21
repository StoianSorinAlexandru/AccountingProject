using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ProiectConta.Gestions
{
    public class GestionAppService : ApplicationService, IGestionAppService
    {
        private readonly IGestionRepository _gestionRepository;
        private readonly GestionManager _gestionManager;
        public GestionAppService(IGestionRepository gestionRepository, GestionManager gestionManager)
        {
            _gestionRepository = gestionRepository;
            _gestionManager = gestionManager;
        }

        public async Task<GestionDto> GetAsync(Guid id)
        {
            var gestion = await _gestionRepository.GetAsync(id);
            return ObjectMapper.Map<Gestion, GestionDto>(gestion);
        }

        public async Task<List<GestionDto>> GetAllAsync()
        {
            var gestions = await _gestionRepository.GetListAsync();
            return ObjectMapper.Map<List<Gestion>, List<GestionDto>>(gestions);
        }
        public async Task<GestionDto> GetGestionAsync(string name)
        {
            var gestion = await _gestionRepository.FindByNameAsync(name);
            return ObjectMapper.Map<Gestion, GestionDto>(gestion);
        }

        public async Task<GestionDto> CreateAsync(CreateUpdateGestionDto input)
        {
            var gestion = await _gestionManager.CreateAsync(
                input.Name
            );
            //await _gestionRepository.InsertAsync(gestion, autoSave: true);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _gestionRepository.InsertAsync(gestion);
                transaction.Complete();
            }

            return ObjectMapper.Map<Gestion, GestionDto>(gestion);
        }

        public async Task UpdateAsync(Guid id, CreateUpdateGestionDto input)
        {
            var gestion = await _gestionRepository.GetAsync(id);
            if (gestion.Name != input.Name)
            {
                await _gestionManager.ChangeNameAsync(gestion, input.Name);
            }
            await _gestionRepository.UpdateAsync(gestion);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _gestionRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<GestionDto>> GetListAsync(GetGestionListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Gestion.Name);
            }
            var gestions = await _gestionRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );
            //var totalCount = await _gestionRepository.GetCountAsync();
            var totalCount = input.Filter == null
                ? await _gestionRepository.CountAsync()
                : await _gestionRepository.CountAsync(
                    gestion => gestion.Name.Contains(input.Filter));

            //Folosim mapper manual pentru a evita problema de scope a OpjectMapper.
            var gestionDtoList = gestions.Select(gestion => new GestionDto
            {
                Id = gestion.Id,
                Name = gestion.Name
            }).ToList();

            return new PagedResultDto<GestionDto>(
                totalCount,
                gestionDtoList
            //ObjectMapper.Map<List<Gestion>, List<GestionDto>>(gestions)
            );
        }

    }
}

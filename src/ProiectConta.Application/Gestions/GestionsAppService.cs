using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ProiectConta.Gestions
{
    public class GestionAppService : ApplicationService, IGestionAppService
    {
        private readonly IGestionRepository _gestionRepository;

        public GestionAppService(IGestionRepository gestionRepository)
        {
            _gestionRepository = gestionRepository;
        }

        public async Task<GestionDto> GetAsync(Guid id)
        {
            var gestion = await _gestionRepository.GetAsync(id);
            return ObjectMapper.Map<Gestion, GestionDto>(gestion);
        }

        public async Task<List<GestionDto>> GetListAsync()
        {
            var gestions = await _gestionRepository.GetListAsync();
            return ObjectMapper.Map<List<Gestion>, List<GestionDto>>(gestions);
        }

        public async Task<GestionDto> CreateAsync(CreateUpdateGestionDto input)
        {
            var gestion = ObjectMapper.Map<CreateUpdateGestionDto, Gestion>(input);
            var createdGestion = await _gestionRepository.InsertAsync(gestion);
            return ObjectMapper.Map<Gestion, GestionDto>(createdGestion);
        }

        public async Task<GestionDto> UpdateAsync(Guid id, CreateUpdateGestionDto input)
        {
            var gestion = await _gestionRepository.GetAsync(id);
            ObjectMapper.Map(input, gestion);
            var updatedGestion = await _gestionRepository.UpdateAsync(gestion);
            return ObjectMapper.Map<Gestion, GestionDto>(updatedGestion);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _gestionRepository.DeleteAsync(id);
        }
    }
}

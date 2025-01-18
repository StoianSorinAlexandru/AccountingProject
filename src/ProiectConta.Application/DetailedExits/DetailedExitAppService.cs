using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ProiectConta.DetailedExits
{
    public class DetailedExitAppService : ApplicationService, IDetailedExitAppService
    {
        private readonly IDetailedExitRepository _detailedExitRepository;

        public DetailedExitAppService(IDetailedExitRepository detailedExitRepository)
        {
            _detailedExitRepository = detailedExitRepository;
        }

        public async Task<DetailedExitDto> GetAsync(Guid id)
        {
            var detailedExit = await _detailedExitRepository.GetAsync(id);
            return ObjectMapper.Map<DetailedExit, DetailedExitDto>(detailedExit);
        }

        public async Task<List<DetailedExitDto>> GetListAsync()
        {
            var detailedExits = await _detailedExitRepository.GetListAsync();
            return ObjectMapper.Map<List<DetailedExit>, List<DetailedExitDto>>(detailedExits);
        }

        public async Task<DetailedExitDto> CreateAsync(CreateUpdateDetailedExitDto input)
        {
            var detailedExit = ObjectMapper.Map<CreateUpdateDetailedExitDto, DetailedExit>(input);
            var createdDetailedExit = await _detailedExitRepository.InsertAsync(detailedExit);
            return ObjectMapper.Map<DetailedExit, DetailedExitDto>(createdDetailedExit);
        }

        public async Task<DetailedExitDto> UpdateAsync(Guid id, CreateUpdateDetailedExitDto input)
        {
            var detailedExit = await _detailedExitRepository.GetAsync(id);
            ObjectMapper.Map(input, detailedExit);
            var updatedDetailedExit = await _detailedExitRepository.UpdateAsync(detailedExit);
            return ObjectMapper.Map<DetailedExit, DetailedExitDto>(updatedDetailedExit);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _detailedExitRepository.DeleteAsync(id);
        }
    }
}

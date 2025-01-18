using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ProiectConta.Exits
{
    public class ExitAppService : ApplicationService, IExitAppService
    {
        private readonly IExitRepository _exitRepository;

        public ExitAppService(IExitRepository exitRepository)
        {
            _exitRepository = exitRepository;
        }

        public async Task<ExitDto> GetAsync(Guid id)
        {
            var exit = await _exitRepository.GetAsync(id);
            return ObjectMapper.Map<Exit, ExitDto>(exit);
        }

        public async Task<List<ExitDto>> GetListAsync()
        {
            var exits = await _exitRepository.GetListAsync();
            return ObjectMapper.Map<List<Exit>, List<ExitDto>>(exits);
        }

        public async Task<ExitDto> CreateAsync(CreateUpdateExitDto input)
        {
            var exit = ObjectMapper.Map<CreateUpdateExitDto, Exit>(input);
            var createdExit = await _exitRepository.InsertAsync(exit);
            return ObjectMapper.Map<Exit, ExitDto>(createdExit);
        }

        public async Task<ExitDto> UpdateAsync(Guid id, CreateUpdateExitDto input)
        {
            var exit = await _exitRepository.GetAsync(id);
            ObjectMapper.Map(input, exit);
            var updatedExit = await _exitRepository.UpdateAsync(exit);
            return ObjectMapper.Map<Exit, ExitDto>(updatedExit);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _exitRepository.DeleteAsync(id);
        }
    }
}

using ProiectConta.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProiectConta.DetailedExits
{
    public class DetailedExitAppService : ApplicationService, IDetailedExitAppService
    {
        private readonly IDetailedExitRepository _detailedExitRepository;
        private readonly IProductRepository _productRepository;
        private readonly DetailedExitManager _detailedExitManager;

        public DetailedExitAppService(IDetailedExitRepository detailedExitRepository, IProductRepository productRepository, DetailedExitManager detailedExitManager)
        {
            _detailedExitRepository = detailedExitRepository;
            _productRepository = productRepository;
            _detailedExitManager = detailedExitManager;
        }

        public async Task<DetailedExitDto> GetAsync(Guid id)
        {
            var detailedExit = await _detailedExitRepository.GetAsync(id);
            return ObjectMapper.Map<DetailedExit, DetailedExitDto>(detailedExit);
        }

        public async Task<DetailedExitDto> FindByExitId(Guid id)
        {
            var detailedExits = await _detailedExitRepository.GetAsync(id);
                return ObjectMapper.Map<DetailedExit, DetailedExitDto>(detailedExits);
        }

        public async Task<DetailedExitDto> CreateAsync(CreateUpdateDetailedExitDto input)
        {
            var product = await _productRepository.FindByNameAsync(input.ProductName);   
            var productPrice = product.Price ?? 0;
            var detailedExit = await _detailedExitManager.CreateAsync(
                input.ExitId,
                product.Id,
                input.Quantity,
                input.Quantity * productPrice // value = quantity * price
            );
            await _detailedExitRepository.InsertAsync(detailedExit, autoSave: true);
            return ObjectMapper.Map<DetailedExit, DetailedExitDto>(detailedExit);
        }

        public async Task UpdateAsync(Guid id, CreateUpdateDetailedExitDto input)
        {
            var detailedExit = await _detailedExitRepository.GetAsync(id);
            var product = await _productRepository.FindByNameAsync(input.ProductName);
            var productPrice = product.Price ?? 0;
            detailedExit.ProductId = product.Id;
            detailedExit.Quantity = input.Quantity;
            detailedExit.Value = input.Quantity * productPrice;
            await _detailedExitRepository.UpdateAsync(detailedExit);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _detailedExitRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<DetailedExitDto>> GetListAsync(GetDetailedExitListDto input)
        {

            var detailedExits = await _detailedExitRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
            );
            var detailedExitDtos = ObjectMapper.Map<List<DetailedExit>, List<DetailedExitDto>>(detailedExits);
            return new PagedResultDto<DetailedExitDto>(
                await _detailedExitRepository.GetCountAsync(),
                detailedExitDtos
            );
        }
    }
}

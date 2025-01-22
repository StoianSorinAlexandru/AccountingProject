using ProiectConta.Partners;
using ProiectConta.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProiectConta.DetailedEntries
{
    public class DetailedEntryAppService : ApplicationService, IDetailedEntryAppService
    {
        private readonly IDetailedEntryRepository _detailedEntryRepository;
        private readonly IProductRepository _productRepository;
        private readonly DetailedEntryManager _detailedEntryManager;

        public DetailedEntryAppService(IDetailedEntryRepository detailedEntryRepository, IProductRepository productRepository, DetailedEntryManager detailedEntryManager)
        {
            _detailedEntryRepository = detailedEntryRepository;
            _productRepository = productRepository;
            _detailedEntryManager = detailedEntryManager;
        }

        public async Task<DetailedEntryDto> GetAsync(Guid id)
        {
            var detailedEntry = await _detailedEntryRepository.GetAsync(id);
            return ObjectMapper.Map<DetailedEntry, DetailedEntryDto>(detailedEntry);
        }

        public async Task<DetailedEntryDto> FindByEntryId(Guid id)
        {
            var detailedEntry = (await _detailedEntryRepository.GetListAsync()).Where(de => de.EntryId == id).FirstOrDefault();

            return ObjectMapper.Map<DetailedEntry, DetailedEntryDto>(detailedEntry);

            //var partners = await _partnerRepository.GetListAsync();
            //return partners.Select(partner => ObjectMapper.Map<Partner, PartnerDto>(partner)).ToList();
        }

        public async Task<DetailedEntryDto> CreateAsync(CreateUpdateDetailedEntryDto input)
        {
            var product = await _productRepository.FindByNameAsync(input.ProductName);
            var productPrice = product.Price ?? 0; 
            var detailedEntry = await _detailedEntryManager.CreateAsync(
                input.EntryId,
                product.Id,
                input.Quantity,
                input.Quantity * productPrice // value = quantity * price
            );
            await _detailedEntryRepository.InsertAsync(detailedEntry, autoSave: true);
            return ObjectMapper.Map<DetailedEntry, DetailedEntryDto>(detailedEntry);
        }

        public async Task UpdateAsync(Guid id, CreateUpdateDetailedEntryDto input)
        {
            var detailedEntry = await _detailedEntryRepository.GetAsync(id);
            var product = await _productRepository.FindByNameAsync(input.ProductName);
            var productPrice = product.Price ?? 0; 
            detailedEntry.ProductId = product.Id;
            detailedEntry.Quantity = input.Quantity;
            detailedEntry.Value = input.Quantity * productPrice; // value = quantity * price
            await _detailedEntryRepository.UpdateAsync(detailedEntry);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _detailedEntryRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<DetailedEntryDto>> GetListAsync(GetDetailedEntryListDto input)
        {
            var detailedEntries = await _detailedEntryRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
            );
            var detailedEntryDtos = ObjectMapper.Map<List<DetailedEntry>, List<DetailedEntryDto>>(detailedEntries);
            return new PagedResultDto<DetailedEntryDto>(
                await _detailedEntryRepository.GetCountAsync(),
                detailedEntryDtos
            );
        }
    }
}

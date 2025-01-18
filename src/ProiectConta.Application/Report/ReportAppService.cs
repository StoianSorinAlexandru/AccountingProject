using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ProiectConta.Reports
{
    public class ReportAppService : ApplicationService, IReportAppService
    {
        private readonly IReportRepository _reportRepository;

        public ReportAppService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<ReportDto> GetAsync(Guid id)
        {
            var report = await _reportRepository.GetAsync(id);
            return ObjectMapper.Map<Report, ReportDto>(report);
        }

        public async Task<List<ReportDto>> GetListAsync()
        {
            var reports = await _reportRepository.GetListAsync();
            return ObjectMapper.Map<List<Report>, List<ReportDto>>(reports);
        }

        public async Task<ReportDto> CreateAsync(CreateUpdateReportDto input)
        {
            var report = ObjectMapper.Map<CreateUpdateReportDto, Report>(input);
            var createdReport = await _reportRepository.InsertAsync(report);
            return ObjectMapper.Map<Report, ReportDto>(createdReport);
        }

        public async Task<ReportDto> UpdateAsync(Guid id, CreateUpdateReportDto input)
        {
            var report = await _reportRepository.GetAsync(id);
            ObjectMapper.Map(input, report);
            var updatedReport = await _reportRepository.UpdateAsync(report);
            return ObjectMapper.Map<Report, ReportDto>(updatedReport);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _reportRepository.DeleteAsync(id);
        }
    }
}

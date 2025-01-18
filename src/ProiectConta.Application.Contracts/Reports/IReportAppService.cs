using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectConta.Reports
{
    public interface IReportAppService
    {
        Task<ReportDto> GetAsync(Guid id);
        Task<List<ReportDto>> GetListAsync();
        Task<ReportDto> CreateAsync(CreateUpdateReportDto input);
        Task<ReportDto> UpdateAsync(Guid id, CreateUpdateReportDto input);
        Task DeleteAsync(Guid id);
    }
}

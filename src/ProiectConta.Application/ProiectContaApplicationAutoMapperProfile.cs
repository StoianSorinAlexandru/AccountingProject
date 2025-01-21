using AutoMapper;
using ProiectConta.DetailedEntries;
using ProiectConta.DetailedExits;
using ProiectConta.Entries;
using ProiectConta.Exits;
using ProiectConta.Gestions;
using ProiectConta.Partners;
using ProiectConta.Products;
using ProiectConta.Reports;

namespace ProiectConta;

public class ProiectContaApplicationAutoMapperProfile : Profile
{
    public ProiectContaApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Gestion, GestionDto>();
        CreateMap<Partner, PartnerDto>();
        CreateMap<Product, ProductDto>();
        CreateMap<Report, ReportDto>();
        CreateMap<Exit, ExitDto>();
        CreateMap<Entry, EntryDto>();
        CreateMap<DetailedExit, DetailedExitDto>();
        CreateMap<DetailedEntry, DetailedEntryDto>();

        CreateMap<PartnerDto, CreateUpdatePartnerDto>();
        CreateMap<GestionDto, CreateUpdateGestionDto>();
        CreateMap<ProductDto, CreateUpdateProductDto>();
        CreateMap<ReportDto, CreateUpdateReportDto>();
        CreateMap<ExitDto, CreateUpdateExitDto>();
        CreateMap<EntryDto, CreateUpdateEntryDto>();
        CreateMap<DetailedExitDto, CreateUpdateDetailedExitDto>();
        CreateMap<DetailedEntryDto, CreateUpdateDetailedEntryDto>();


    }
}

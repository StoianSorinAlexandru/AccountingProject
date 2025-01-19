using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Mvc;
using ProiectConta.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProiectConta.Blazor.Pages;

public partial class Partners
{
    private IReadOnlyList<PartnerDto> PartnerList { get; set; }
    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }


    private CreateUpdatePartnerDto NewPartner { get; set; }
    private Guid EditingPartnerId { get; set; }
    private CreateUpdatePartnerDto EditingPartner { get; set; }

    private Modal CreatePartnerModal { get; set; }
    private Modal EditPartnerModal { get; set; }

    public Partners()
    {
        NewPartner = new CreateUpdatePartnerDto();
        EditingPartner = new CreateUpdatePartnerDto();
    }

    protected override async Task OnInitializedAsync()
    {
        await GetPartnersAsync();
    }

    private async Task GetPartnersAsync()
    {
        var result = await PartnerAppService.GetListAsync(
            new GetPartnerListDto
            {
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting
            }
        );
        PartnerList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    private async Task InDataGridReadAsync(DataGridReadDataEventArgs<PartnerDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " desc" : ""))
            .JoinAsString(",");
        CurrentPage = e.Page;
        await GetPartnersAsync();
    }

    private void OpenCreatePartnerModal()
    {
        NewPartner = new CreateUpdatePartnerDto();
        CreatePartnerModal.Show();
    }

    private void CloseCreatePartnerModal()
    {
        CreatePartnerModal.Hide();
    }

    private void OpenEditPartnerModal(PartnerDto partner)
    {
        EditingPartnerId = partner.Id;
        EditingPartner = ObjectMapper.Map<PartnerDto, CreateUpdatePartnerDto>(partner);
        EditPartnerModal.Show();
    }

    private void CloseEditPartnerModal()
    {
        EditPartnerModal.Hide();
    }

    private async Task CreatePartnerAsync()
    {
        await PartnerAppService.CreateAsync(NewPartner);
        await GetPartnersAsync();
        CloseCreatePartnerModal();
    }

    private async Task UpdatePartnerAsync()
    {
        await PartnerAppService.UpdateAsync(EditingPartnerId, EditingPartner);
        await GetPartnersAsync();
        CloseEditPartnerModal();
    }

    private async Task DeletePartnerAsync(PartnerDto partner)
    {
        await PartnerAppService.DeleteAsync(partner.Id);
        await GetPartnersAsync();
    }

}
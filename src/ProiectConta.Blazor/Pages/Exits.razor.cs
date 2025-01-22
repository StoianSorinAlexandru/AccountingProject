using Blazorise;
using Blazorise.DataGrid;
using ProiectConta.DetailedExits;
using ProiectConta.Exits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProiectConta.Blazor.Pages;

public partial class Exits
{
    private IReadOnlyList<ExitDto> ExitList { get; set; }
    private IReadOnlyList<string> PartnerList { get; set; }
    private IReadOnlyList<string> GestionList { get; set; }
    private IReadOnlyList<string> ProductList { get; set; }

    private int PageSize { get; set; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }

    private CreateUpdateExitDto NewExit { get; set; }
    private CreateUpdateDetailedExitDto NewDetailedExit { get; set; }
    private Guid EditingExitId { get; set; }
    private Guid EditingDetailedExitId { get; set; }
    private CreateUpdateExitDto EditingExit { get; set; }
    private CreateUpdateDetailedExitDto EditingDetailedExit { get; set; }
    private ExitDto SelectedExit { get; set; }
    private DetailedExitDto DetailedExit { get; set; }

    private Modal CreateExitModal { get; set; }
    private Modal EditExitModal { get; set; }
    private Modal ViewDetailsModal { get; set; }

    public Exits() 
    {
        NewExit = new CreateUpdateExitDto();
        EditingExit = new CreateUpdateExitDto();
    }

    protected override async Task OnInitializedAsync()
    {
        PartnerList = new List<string>();
        GestionList = new List<string>();
        ProductList = new List<string>();
        NewDetailedExit = new CreateUpdateDetailedExitDto();
        EditingDetailedExit= new CreateUpdateDetailedExitDto();
        await GetExitsAsync();
        await GetPartnersAsync();
        await GetGestionsAsync();
        await GetProductsAsync();
    }

    private async Task GetExitsAsync()
    {
        var result = await ExitAppService.GetListAsync(
            new GetExitListDto
            {
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting
            }
        );
        ExitList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    private async Task GetPartnersAsync()
    {
        var result = await PartnerAppService.GetAllAsync();
        PartnerList = result.Select(p => p.Name).ToList();
    }

    private async Task GetGestionsAsync()
    {
        var result = await GestionAppService.GetAllAsync();
        GestionList = result.Select(g => g.Name).ToList();
    }

    private async Task GetProductsAsync()
    {
        var result = await ProductAppService.GetAllAsync();
        ProductList = result.Select(p => p.Name).ToList();
    }

    private async Task InDataGridReadAsync(DataGridReadDataEventArgs<ExitDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Ascending ? " ASC" : " DESC"))
            .JoinAsString(",");
        CurrentPage = e.Page - 1;
        await GetExitsAsync();
    }

    private void OpenCreateExitModal()
    {
        NewExit = new CreateUpdateExitDto();
        NewExit.Date = DateTime.Now;
        NewExit.PartnerName = PartnerList.FirstOrDefault();
        NewExit.GestionName = GestionList.FirstOrDefault();

        NewDetailedExit = new CreateUpdateDetailedExitDto();
        NewDetailedExit.ProductName = ProductList.FirstOrDefault();
        NewDetailedExit.Quantity = 1;

        CreateExitModal.Show();
    }

    private async void OpenEditExitModal(ExitDto exit)
    {
        EditingExitId = exit.Id;
        EditingExit = ObjectMapper.Map<ExitDto, CreateUpdateExitDto>(exit);

        EditingDetailedExitId = exit.Id;
        var detailedExit = await DetailedExitAppService.FindByExitId(exit.Id);
        EditingDetailedExit = ObjectMapper.Map<DetailedExitDto, CreateUpdateDetailedExitDto>(detailedExit);

        EditExitModal.Show();
    }

    private void OpenViewDetailsModal(ExitDto exit)
    {
        SelectedExit = exit;
        ViewDetailsModal.Show();
    }

    private void CloseCreateExitModal()
    {
        CreateExitModal.Hide();
    }

    private void CloseEditExitModal()
    {
        EditExitModal.Hide();
    }

    private void CloseViewDetailsModal()
    {
        ViewDetailsModal.Hide();
    }

    private async Task CreateExitAsync()
    {
        var createdExit = await ExitAppService.CreateAsync(NewExit);
        NewDetailedExit.ExitId = createdExit.Id;
        await DetailedExitAppService.CreateAsync(NewDetailedExit);
        NewDetailedExit = new CreateUpdateDetailedExitDto();
        await GetExitsAsync();
        CreateExitModal.Hide();
    }

    private async Task UpdateExitAsync()
    {
        await ExitAppService.UpdateAsync(EditingExitId, EditingExit);
        EditingExit = new CreateUpdateExitDto();
        await GetExitsAsync();
        EditExitModal.Hide();
    }

    private async Task DeleteExitAsync(ExitDto exit)
    {
        await ExitAppService.DeleteAsync(exit.Id);
        await GetExitsAsync();
    }
}


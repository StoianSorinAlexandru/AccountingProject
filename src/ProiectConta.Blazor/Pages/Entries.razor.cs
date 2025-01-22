using Blazorise;
using Blazorise.DataGrid;
using ProiectConta.DetailedEntries;
using ProiectConta.Entries;
using ProiectConta.Gestions;
using ProiectConta.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProiectConta.Blazor.Pages;

public partial class Entries
{
    private IReadOnlyList<EntryDto> EntryList { get; set; }
    private IReadOnlyList<string> PartnerList { get; set; }
    private IReadOnlyList<string> GestionList { get; set; }

    private IReadOnlyList<string> ProductList { get; set; }

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }

    private CreateUpdateEntryDto NewEntry { get; set; }
    private CreateUpdateDetailedEntryDto NewDetailedEntry { get; set; }
    private Guid EditingEntryId { get; set; }
    private Guid EditingDetailedEntryId { get; set; }
    private CreateUpdateEntryDto EditingEntry { get; set; }
    private CreateUpdateDetailedEntryDto EditingDetailedEntry { get; set; }
    private EntryDto SelectedEntry { get; set; }
    private DetailedEntryDto DetailedEntry { get; set; }

    private Modal CreateEntryModal { get; set; }
    private Modal EditEntryModal { get; set; }
    private Modal ViewDetailsModal { get; set; }

    public Entries()
    {
        NewEntry = new CreateUpdateEntryDto();
        EditingEntry = new CreateUpdateEntryDto();
    }

    protected override async Task OnInitializedAsync()
    {
        PartnerList = new List<string>();
        GestionList = new List<string>();
        ProductList = new List<string>();
        NewDetailedEntry = new CreateUpdateDetailedEntryDto();
        EditingDetailedEntry = new CreateUpdateDetailedEntryDto();
        await GetEntriesAsync();
        await GetPartnersAsync();
        await GetGestionsAsync();
        await GetProductsAsync();
    }

    private async Task GetEntriesAsync()
    {
        var result = await EntryAppService.GetListAsync(
            new GetEntryListDto
            {
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting
            }
        );
        EntryList = result.Items;
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
        ProductList = result.Select(product => product.Name).ToList();
    }

    private async Task InDataGridReadAsync(DataGridReadDataEventArgs<EntryDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Ascending ? " ASC" : " DESC"))
            .JoinAsString(",");
        CurrentPage = e.Page - 1;
        await GetEntriesAsync();
    }

    private void OpenCreateEntryModal()
    {
        NewEntry = new CreateUpdateEntryDto();
        NewEntry.Date = DateTime.Now;
        NewEntry.PartnerName = PartnerList.First();
        NewEntry.GestionName = GestionList.First();

        NewDetailedEntry = new CreateUpdateDetailedEntryDto();
        NewDetailedEntry.ProductName = ProductList.First();
        NewDetailedEntry.Quantity = 1;

        CreateEntryModal.Show();
    }

    private void CloseCreateEntryModal()
    {
        CreateEntryModal.Hide();
    }

    private async void OpenEditEntryModal(EntryDto entry)
    {
        EditingEntryId = entry.Id;
        EditingEntry = ObjectMapper.Map<EntryDto, CreateUpdateEntryDto>(EntryList.First(e => e.Id == entry.Id));

        EditingDetailedEntryId = entry.Id;
        var detailedEntry = await DetailedEntryAppService.FindByEntryId(entry.Id);
        EditingDetailedEntry = ObjectMapper.Map<DetailedEntryDto, CreateUpdateDetailedEntryDto>(detailedEntry);

        EditEntryModal.Show();
    }


    private void CloseEditEntryModal()
    {
        EditEntryModal.Hide();
    }
    private void OpenViewDetailsModal(EntryDto entry)
    {
        DetailedEntry = DetailedEntryAppService.FindByEntryId(entry.Id).Result;
        SelectedEntry = entry;
        ViewDetailsModal.Show();
    }

    private void CloseViewDetailsModal()
    {
        ViewDetailsModal.Hide();
    }

    private async Task CreateEntryAsync()
    {
        var createdEntry = await EntryAppService.CreateAsync(NewEntry);
        NewDetailedEntry.EntryId = createdEntry.Id;
        await DetailedEntryAppService.CreateAsync(NewDetailedEntry);
        NewEntry = new CreateUpdateEntryDto();
        await GetEntriesAsync();
        await CreateEntryModal.Hide();
    }

    private async Task UpdateEntryAsync()
    {
        await EntryAppService.UpdateAsync(EditingEntryId, EditingEntry);
        EditingEntry = new CreateUpdateEntryDto();
        await GetEntriesAsync();
        await EditEntryModal.Hide();
    }

    private async Task DeleteEntryAsync(EntryDto entry)
    {
        await EntryAppService.DeleteAsync(entry.Id);
        await GetEntriesAsync();
    }

}
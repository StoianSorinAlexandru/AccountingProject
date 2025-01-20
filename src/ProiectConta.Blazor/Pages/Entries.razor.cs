using Blazorise;
using Blazorise.DataGrid;
using ProiectConta.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProiectConta.Blazor.Pages;

public partial class Entries
{
    private IReadOnlyList<EntryDto> EntryList { get; set; }
    
    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }

    private CreateUpdateEntryDto NewEntry { get; set; }
    private Guid EditingEntryId { get; set; }
    private CreateUpdateEntryDto EditingEntry { get; set; }

    private Modal CreateEntryModal { get; set; }
    private Modal EditEntryModal { get; set; }

    public Entries()
    {
        NewEntry = new CreateUpdateEntryDto();
        EditingEntry = new CreateUpdateEntryDto();
    }

    protected override async Task OnInitializedAsync()
    {
        await GetEntriesAsync();
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
        CreateEntryModal.Show();
    }

    private void CloseCreateEntryModal()
    {
        CreateEntryModal.Hide();
    }

    private void OpenEditEntryModal(Guid id)
    {
        EditingEntryId = id;
        EditingEntry = ObjectMapper.Map<EntryDto, CreateUpdateEntryDto>(EntryList.First(e => e.Id == id));
        EditEntryModal.Show();
    }

    private void CloseEditEntryModal()
    {
        EditEntryModal.Hide();
    }

    private async Task CreateEntryAsync()
    {
        await EntryAppService.CreateAsync(NewEntry);
        NewEntry = new CreateUpdateEntryDto();
        await GetEntriesAsync();
        CreateEntryModal.Hide();
    }

    private async Task UpdateEntryAsync()
    {
        await EntryAppService.UpdateAsync(EditingEntryId, EditingEntry);
        EditingEntry = new CreateUpdateEntryDto();
        await GetEntriesAsync();
        EditEntryModal.Hide();
    }

    private async Task DeleteEntryAsync(Guid id)
    {
        await EntryAppService.DeleteAsync(id);
        await GetEntriesAsync();
    }

}
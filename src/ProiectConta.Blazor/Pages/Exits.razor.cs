using Blazorise;
using Blazorise.DataGrid;
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
    private int PageSize { get; set; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }

    private CreateUpdateExitDto NewExit { get; set; }
    private CreateUpdateExitDto EditingExit { get; set; }
    private Guid EditingExitId { get; set; }

    private Modal CreateExitModal { get; set; }
    private Modal EditExitModal { get; set; }

    public Exits() 
    {
        NewExit = new CreateUpdateExitDto();
        EditingExit = new CreateUpdateExitDto();
    }

    protected override async Task OnInitializedAsync()
    {
        await GetExitsAsync();
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
        CreateExitModal.Show();
    }

    private void OpenEditExitModal(ExitDto exit)
    {
        EditingExit = ObjectMapper.Map<ExitDto, CreateUpdateExitDto>(exit);
        EditingExitId = exit.Id;
        EditExitModal.Show();
    }

    private void CloseCreateExitModal()
    {
        CreateExitModal.Hide();
    }

    private void CloseEditExitModal()
    {
        EditExitModal.Hide();
    }

    private async Task CreateExitAsync()
    {
        await ExitAppService.CreateAsync(NewExit);
        await GetExitsAsync();
        CreateExitModal.Hide();
    }

    private async Task UpdateExitAsync()
    {
        await ExitAppService.UpdateAsync(EditingExitId, EditingExit);
        await GetExitsAsync();
        EditExitModal.Hide();
    }

    private async Task DeleteExitAsync(Guid id)
    {
        await ExitAppService.DeleteAsync(id);
        await GetExitsAsync();
    }
}


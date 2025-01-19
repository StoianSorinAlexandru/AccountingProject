using AutoMapper.Internal.Mappers;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authentication;
using ProiectConta.Gestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Volo.Abp.Application.Dtos;
using static System.Net.WebRequestMethods;

namespace ProiectConta.Blazor.Pages
{
    public partial class Gestions
    {
        private IReadOnlyList<GestionDto> GestionList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        private CreateUpdateGestionDto NewGestion { get; set; }
        private Guid EditingGestionId { get; set; }
        private CreateUpdateGestionDto EditingGestion { get; set; }

        private Modal CreateGestionModal { get; set; }
        private Modal EditGestionModal { get; set; }

        public Gestions()
        {
            NewGestion = new CreateUpdateGestionDto();
            EditingGestion = new CreateUpdateGestionDto();
        }

        protected override async Task OnInitializedAsync()
        {
            await GetGestionsAsync();
        }

        private async Task GetGestionsAsync()
        {
            var result = await GestionAppService.GetListAsync(
                new GetGestionListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting
                }
            );
            GestionList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task InDataGridReadAsync(DataGridReadDataEventArgs<GestionDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");

            CurrentPage = e.Page - 1;
            await GetGestionsAsync();
        }
        
        private void OpenCreateGestionModal()
        {
            NewGestion = new CreateUpdateGestionDto();
            CreateGestionModal.Show();
        }

        private void CloseCreateGestionModal()
        {
            CreateGestionModal.Hide();
        }

        private void OpenEditGestionModal(GestionDto gestion)
        {
            EditingGestionId = gestion.Id;
            EditingGestion = ObjectMapper.Map<GestionDto, CreateUpdateGestionDto>(gestion);
            EditGestionModal.Show();
        }

        private void CloseEditGestionModal()
        {
            EditGestionModal.Hide();
        }

        private async Task DeleteGestionAsync(GestionDto gestion)
        {
            await GestionAppService.DeleteAsync(gestion.Id);
            await GetGestionsAsync();
        }

        private async Task CreateGestionAsync()
        {
            await GestionAppService.CreateAsync(NewGestion);
            await GetGestionsAsync();
            CreateGestionModal.Hide();
        }

        private async Task UpdateGestionAsync()
        {
            await GestionAppService.UpdateAsync(EditingGestionId, EditingGestion);
            await GetGestionsAsync();
            EditGestionModal.Hide();
        }
    }
}

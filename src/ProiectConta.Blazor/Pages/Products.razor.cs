using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authentication;
using ProiectConta.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Volo.Abp.Application.Dtos;
using static System.Net.WebRequestMethods;

namespace ProiectConta.Blazor.Pages;

public partial class Products
{

    private IReadOnlyList<ProductDto> ProductList { get; set; }

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }
    
    private CreateUpdateProductDto NewProduct { get; set; }
    private Guid EditingProductId { get; set; }
    private CreateUpdateProductDto EditingProduct { get; set; }

    private Modal CreateProductModal { get; set; }
    private Modal EditProductModal { get; set; }


    public Products() {
        NewProduct = new CreateUpdateProductDto();
        EditingProduct = new CreateUpdateProductDto();
    }

    protected override async Task OnInitializedAsync()
    {
        await GetProductsAsync();
    }

    private async Task GetProductsAsync()
    {
        var result = await ProductAppService.GetListAsync(
            new GetProductListDto
            {
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting
            }
        );
        ProductList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    private async Task InDataGridReadAsync(DataGridReadDataEventArgs<ProductDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");

        CurrentPage = e.Page - 1;

        await GetProductsAsync();
        await InvokeAsync(StateHasChanged);

    }

    private void OpenCreateProductModal()
    {
        NewProduct = new CreateUpdateProductDto();
        CreateProductModal.Show();
    }

    private void CloseCreateProductModal()
    {
        CreateProductModal.Hide();
    }

    private void OpenEditProductModal(ProductDto product)
    {
        EditingProductId = product.Id;
        EditingProduct = ObjectMapper.Map<ProductDto, CreateUpdateProductDto>(product);
        EditProductModal.Show();
    }

    private void CloseEditProductModal()
    {
        EditProductModal.Hide();
    }

    private async Task DeleteProductAsync(ProductDto product)
    {
        await ProductAppService.DeleteAsync(product.Id);
        await GetProductsAsync();
    }

    private async Task CreateProductAsync()
    {
        await ProductAppService.CreateAsync(NewProduct);
        await GetProductsAsync();
        CloseCreateProductModal();
    }

    private async Task UpdateProductAsync()
    {
        await ProductAppService.UpdateAsync(EditingProductId, EditingProduct);
        await GetProductsAsync();
        CloseEditProductModal();
    }
}
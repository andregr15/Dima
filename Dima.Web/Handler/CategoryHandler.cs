using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handler;

public class CategoryHandler(IHttpClientFactory httpClientFactory) : ICategoryHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClienteName);

    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/categories", request);
        return await result.Content.ReadFromJsonAsync<Response<Category?>>()
            ?? new Response<Category?>(null, 400, "Falha ao criar a categoria");
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        var result = await _client.PutAsJsonAsync($"/v1/categories/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Category?>>()
            ?? new Response<Category?>(null, 400, "Falha o atualizar a categoria");
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        var result = await _client.DeleteAsync($"/v1/categories/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Category?>>()
            ?? new Response<Category?>(null, 400, "Falha ao remover a categoria");
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request) =>
        await _client.GetFromJsonAsync<Response<Category?>>($"/v1/categories/{request.Id}")
            ?? new Response<Category?>(null, 400, "Não foi possível obter a categoria");


    public async Task<PagedResponse<IEnumerable<Category>?>> GetAllAsync(GetAllCategoriesRequest request) =>
        await _client.GetFromJsonAsync<PagedResponse<IEnumerable<Category>?>>($"/v1/categories?pageNumber={request.PageNumber}&pageSize={request.PageSize}")
            ?? new PagedResponse<IEnumerable<Category>?>(null, 400, "Não foi possível obter a categorias");




}
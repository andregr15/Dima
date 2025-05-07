using Dima.Core.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handler;

public class TransactionHandler(IHttpClientFactory factory) : ITransactionHandler
{
    private readonly HttpClient _client = factory.CreateClient(Configuration.HttpClienteName);

    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        var result = await _client.PostAsJsonAsync("/v1/transactions", request);
        return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
            ?? new Response<Transaction?>(null, 400, "Não foi possível criar a sua transação");
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        var result = await _client.PutAsJsonAsync($"/v1/transactions/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
            ?? new Response<Transaction?>(null, 400, "Não foi possível atualizar a transação");
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        var result = await _client.DeleteAsync($"/v1/transactions/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
            ?? new Response<Transaction?>(null, 400, "Não foi possível remover a transação");
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request) =>
        await _client.GetFromJsonAsync<Response<Transaction?>>($"/v1/transactions/{request.Id}")
            ?? new Response<Transaction?>(null, 400, "Não foi possível obter a transação");

    public async Task<PagedResponse<IEnumerable<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
    {
        // DateTime -> Structs -> Tipo de Valor -> Valor Padrão
        // 01/01/1900
        const string format = "yyyy-MM-dd";
        var startDate = request.StartDate is not null
            ? request.StartDate.Value.ToString(format)
            : DateTime.Now.FirstDayInMonth().ToString(format);

        var endDate = request.EndDate is not null
            ? request.EndDate.Value.ToString(format)
            : DateTime.Now.LastDayInMonth().ToString(format);

        var url = $"/v1/transactions?startDate={startDate}&endDate={endDate}&pageSize={request.PageSize}&pageNumber={request.PageNumber}";
        var result = await _client.GetFromJsonAsync<PagedResponse<IEnumerable<Transaction>?>>(url);

        return result
            ?? new PagedResponse<IEnumerable<Transaction>?>(
                null,
                400,
                "Não foi possível obter as transações por período"
            );
    }
}

using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Common.Endpoints.Transactions;

public class GetTransactionsByPeriodEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapGet("", HandleAsync)
            .WithName("Transactions: Get By Period")
            .WithSummary("Gets Transactions by period")
            .WithName("Gets Transactions by period")
            .WithOrder(5)
            .Produces<PagedResponse<IEnumerable<Transaction>?>>();

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        DateTime? startDate,
        DateTime? endDate ,
        int pageNumber = Configuration.DefaultPageNumber,
        int pageSize = Configuration.DefaultPageSize
    )
    {
        startDate ??= DateTime.Now.FirstDayInMonth();
        endDate ??= DateTime.Now.LastDayInMonth();
            
        var request = new GetTransactionsByPeriodRequest
        {
            UserId = "1",
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate
        };

        var response = await handler.GetByPeriodAsync(request);

        return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}

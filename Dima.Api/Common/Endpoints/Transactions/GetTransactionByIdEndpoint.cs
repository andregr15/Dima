using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Common.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapGet("{id}", HandleAsync)
            .WithName("Transactions: Get By Id")
            .WithSummary("Gets a Transaction")
            .WithDescription("Gets a Transaction")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();

    public static async Task<IResult> HandleAsync(ITransactionHandler handler, long id)
    {
        var request = new GetTransactionByIdRequest { Id = id, UserId = "1" };
        var response = await handler.GetByIdAsync(request);

        return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}

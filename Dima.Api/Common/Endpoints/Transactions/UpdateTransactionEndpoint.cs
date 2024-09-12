using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Common.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPut("{id}", HandleAsync)
            .WithName("Transactions: Update")
            .WithSummary("Updates a Transaction")
            .WithName("Updates a Transaction")
            .WithOrder(2)
            .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        long id,
        UpdateTransactionRequest request
    )
    {
        request.Id = id;
        request.UserId = "1";
        var response = await handler.UpdateAsync(request);

        return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}

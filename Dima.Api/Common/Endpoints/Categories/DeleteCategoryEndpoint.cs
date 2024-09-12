using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Common.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapDelete("{id}", HandleAsync)
            .WithName("Categories: Delete")
            .WithSummary("Deletes a Category")
            .WithDescription("Deletes a Category")
            .WithOrder(3)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(long id, ICategoryHandler handler)
    {
        var request = new DeleteCategoryRequest { Id = id, UserId = "1" };
        var response = await handler.DeleteAsync(request);
        return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}

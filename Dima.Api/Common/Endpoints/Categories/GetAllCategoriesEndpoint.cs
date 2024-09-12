using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Common.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapGet("", HandleAsync)
            .WithName("Categories: Get All")
            .WithSummary("Gets all Categories")
            .WithDescription("Gets all Categories")
            .WithOrder(5)
            .Produces<PagedResponse<IEnumerable<Category>?>>();

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        int pageNumber = Configuration.DefaultPageNumber,
        int pageSize = Configuration.DefaultPageSize
    )
    {
        var request = new GetAllCategoriesRequest
        {
            UserId = "1",
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var response = await handler.GetAllAsync(request);

        return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}

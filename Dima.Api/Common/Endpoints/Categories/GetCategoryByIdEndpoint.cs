using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Common.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapGet("{id}", HandleAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Gets a Category")
            .WithDescription("Gets a Category")
            .WithOrder(4)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(long id, ICategoryHandler handler)
    {
        var request = new GetCategoryByIdRequest { Id = id, UserId = "1" };
        var response = await handler.GetByIdAsync(request);

        return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}
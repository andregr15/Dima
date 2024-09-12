using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Common.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPost("", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Creates a new Category")
            .WithDescription("Creates a new Category")
            .WithOrder(1)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        CreateCategoryRequest request
    )
    {
        var response = await handler.CreateAsync(request);

        return response.IsSuccess
            ? TypedResults.Created($"/{response.Data?.Id}", response)
            : TypedResults.BadRequest(response);
    }
}

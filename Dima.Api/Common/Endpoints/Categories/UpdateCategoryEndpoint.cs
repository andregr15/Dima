using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Common.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPut("{id}", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Updates a Category")
            .WithDescription("Updates a Category")
            .WithOrder(2)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        UpdateCategoryRequest request,
        ICategoryHandler handler,
        long id
    )
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;
        var response = await handler.UpdateAsync(request);
        return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}

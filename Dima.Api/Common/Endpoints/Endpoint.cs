using Dima.Api.Common.Api;
using Dima.Api.Common.Endpoints.Categories;
using Dima.Api.Common.Endpoints.Identity;
using Dima.Api.Common.Endpoints.Transactions;
using Dima.Api.Models;

namespace Dima.Api.Common.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        app.MapGet("/", () => new { message = "Ok" }).WithTags("Health Check");

        endpoints
            .MapGroup("v1/categories")
            .WithTags("Categories")
            .RequireAuthorization()
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>()
            .MapEndpoint<GetCategoryByIdEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>();

        endpoints
            .MapGroup("v1/transactions")
            .WithTags("Transactions")
            .RequireAuthorization()
            .MapEndpoint<CreateTransactionEndpoint>()
            .MapEndpoint<UpdateTransactionEndpoint>()
            .MapEndpoint<DeleteTransactionEndpoint>()
            .MapEndpoint<GetTransactionByIdEndpoint>()
            .MapEndpoint<GetTransactionsByPeriodEndpoint>();

        endpoints.MapGroup("v1/identity").WithTags("Identity").MapIdentityApi<User>();

        endpoints
            .MapGroup("v1/identity")
            .WithTags("Identity")
            .RequireAuthorization()
            .MapEndpoint<LogoutEndpoint>()
            .MapEndpoint<GetRolesEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}

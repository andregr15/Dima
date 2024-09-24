using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Api.Common.Endpoints.Categories;
using Dima.Api.Common.Endpoints.Transactions;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Dima.Api.Common.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

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

        var identityEndpoints = endpoints.MapGroup("v1/indentity");

        identityEndpoints.WithTags("Identity").MapIdentityApi<User>();
        identityEndpoints
            .MapPost(
                "logout",
                async (SignInManager<User> signInManager, UserManager<User> userManager) =>
                {
                    await signInManager.SignOutAsync();
                    return Results.Ok();
                }
            )
            .RequireAuthorization();

        identityEndpoints
            .MapGet(
                "roles",
                (ClaimsPrincipal user) =>
                {
                    if (user.Identity is null || !user.Identity.IsAuthenticated)
                        return Results.Unauthorized();

                    var identity = (ClaimsIdentity)user.Identity;
                    var roles = identity
                        .FindAll(identity.RoleClaimType)
                        .Select(c => new
                        {
                            c.Issuer,
                            c.OriginalIssuer,
                            c.Type,
                            c.Value,
                            c.ValueType
                        });

                    return TypedResults.Json(roles);
                }
            )
            .RequireAuthorization();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}

using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Api.Models;
using Dima.Core;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Extensions;

public static class BuilderExtensions
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString =
            builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

        Configuration.BackendUrl =
            builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuration.FrontendUrl =
            builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }

    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(Configuration.ConnectionString)
        );
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(n => n.FullName));
    }

    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        builder
            .Services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddApplicationCookie();
        builder.Services.AddAuthorization();
    }

    public static void AddIdentity(this WebApplicationBuilder builder)
    {
        builder
            .Services.AddIdentityCore<User>()
            .AddRoles<IdentityRole<long>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();
    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
            options.AddPolicy(
                ApiConfiguration.CorsPolicyName,
                policy =>
                    policy
                        .WithOrigins([Configuration.BackendUrl, Configuration.FrontendUrl])
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
            )
        );
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
    }
}

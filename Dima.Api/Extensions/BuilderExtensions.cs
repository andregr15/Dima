using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Extensions;

public static  class BuilderExtensions
{
    public static void AddDbContext(this WebApplicationBuilder builder)
    {
        var cnnStr = builder
            .Configuration
            .GetConnectionString("DefaultConnection") 
            ?? string.Empty;

        builder.Services.AddDbContext<AppDbContext>(
            options => options.UseSqlite(cnnStr)
        );
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
    }
}

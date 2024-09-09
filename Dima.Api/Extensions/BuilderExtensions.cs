using Dima.Api.Data;
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
}

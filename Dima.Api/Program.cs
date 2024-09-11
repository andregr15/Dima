using Dima.Api.Extensions;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbContext();
builder.AddServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(n => n.FullName));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.MapPost(
        "v1/categories",
        (CreateCategoryRequest request, ICategoryHandler handler) => handler.CreateAsync(request)
    )
    .WithName("Categories: Create")
    .WithDescription("Creates a new Category")
    .Produces<Response<Category?>>();

app.MapPut(
        "/v1/categories/{id}",
        async (long id, UpdateCategoryRequest request, ICategoryHandler handler) =>
        {
            request.Id = id;
            return await handler.UpdateAsync(request);
        }
    )
    .WithName("Categories: Update")
    .WithDescription("Updates a Category")
    .Produces<Response<Category?>>();

app.MapDelete(
        "/v1/categories/{id}",
        async (long id, ICategoryHandler handler) =>
        {
            var request = new DeleteCategoryRequest { Id = id, UserId = "1" };
            return await handler.DeleteAsync(request);
        }
    )
    .WithName("Categories: Delete")
    .WithDescription("Deletes a Category")
    .Produces<Response<Category?>>();

app.MapGet(
        "v1/categories/{id}",
        async (long id, ICategoryHandler handler) =>
        {
            var request = new GetCategoryByIdRequest { Id = id, UserId = "1" };
            return await handler.GetByIdAsync(request);
        }
    )
    .WithName("Categories: Gets by id")
    .WithDescription("Gets a Category by Id")
    .Produces<Response<Category?>>();

app.MapGet(
        "v1/categories",
        async (
            ICategoryHandler handler,
            int pageNumber = Configuration.DefaultPageNumber,
            int pageSize = Configuration.DefaultPageSize
        ) =>
        {
            var request = new GetAllCategoriesRequest
            {
                UserId = "1",
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            return await handler.GetAllAsync(request);
        }
    )
    .WithName("Categories: Get All")
    .WithDescription("Gets all user's categories")
    .Produces<PagedResponse<IEnumerable<Category>?>>();

app.Run();

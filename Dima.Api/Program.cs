using Dima.Api.Common.Endpoints;
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

app.MapEndpoints();

app.MapGet("/", () => new { message = "Ok" });

app.Run();

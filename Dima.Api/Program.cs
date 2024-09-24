using Dima.Api.Common.Endpoints;
using Dima.Api.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddApplicationCookie();
builder.Services.AddAuthorization();

builder.AddDbContext();
builder.AddIdentity();
builder.AddServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(n => n.FullName));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEndpoints();

app.MapGet("/", () => new { message = "Ok" });

app.Run();

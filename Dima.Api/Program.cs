using Dima.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbContext();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options => options.CustomSchemaIds(n => n.FullName)
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.Run();

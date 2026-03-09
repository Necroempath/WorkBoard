
using WorkBoard.API.Extensions;
using WorkBoard.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddValidation()
    .AddAuthenticationServices(builder.Configuration)
    .AddSwaggerServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerServices();
}

app.UseInfrastructure();

app.MapControllers();

await app.EnsureRoleSeededAsync();

app.Run();

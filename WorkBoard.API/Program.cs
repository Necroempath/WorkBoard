
using WorkBoard.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddAuthenticationServices(builder.Configuration)
    .AddSwaggerServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerServices();
}

app.UseInfrastructure();

app.MapControllers();

app.Run();

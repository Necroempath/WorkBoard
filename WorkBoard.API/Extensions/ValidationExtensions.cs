using FluentValidation.AspNetCore;
using FluentValidation;
using WorkBoard.Application;

namespace WorkBoard.API.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddValidatorsFromAssembly(typeof(ApplicationMarker).Assembly);

        return services;
    }
}

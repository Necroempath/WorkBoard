using WorkBoard.Application;

namespace WorkBoard.API.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ApplicationMarker).Assembly));

        services.AddAutoMapper(typeof(ApplicationMarker).Assembly);

        return services;
    }
}

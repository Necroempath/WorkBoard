namespace WorkBoard.API.Extensions;

public static class MiddlewareExtensions
{
    public static void UseInfrastructure(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
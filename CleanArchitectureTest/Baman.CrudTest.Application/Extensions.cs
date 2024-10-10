using Baman.CrudTest.Application.Basic;
using Microsoft.Extensions.DependencyInjection;

namespace Baman.CrudTest.Application;

public static class Extensions
{
    public static void InstallMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(MetaResponse<>).Assembly));
     //   services.AddMediatR(typeof(MetaResponse<>).Assembly);
    }
}
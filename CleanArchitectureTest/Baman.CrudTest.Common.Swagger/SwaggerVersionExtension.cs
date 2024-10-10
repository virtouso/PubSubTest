using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Baman.CrudTest.Common.Swagger
{
    public static class SwaggerVersionExtension
    {
        public static IServiceCollection AddSwaggerVersion(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
        {
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                });
            services.AddApiVersioning(
                    options =>
                    {
                        options.ReportApiVersions = true;
                        options.AssumeDefaultVersionWhenUnspecified = true;
                        options.DefaultApiVersion = new ApiVersion(1, 0);
                    })
                .AddMvc(
                    options => { options.RespectBrowserAcceptHeader = true; })
                .AddXmlSerializerFormatters();
            return services;
        }
    }



}
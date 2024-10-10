using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baman.CrudTest.Common.Swagger
{
    public static class SwaggerExtensions
    {


        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
        {
            var options = new SwaggerOptions();
            var section = configuration.GetSection(SwaggerOptions.ConfiguraionName);
            section.Bind(options);
            services.Configure<SwaggerOptions>(section);

            //TODO: check if swagger option not configured then thro ArgumentNullExecption
         
            if (options.Disable)
                return services;

            var schemes = new List<OpenApiSecurityScheme> {};

            services.AddSwaggerGen(option =>
            {
            

                foreach (var schema in schemes)
                    option.AddSecurityDefinition(schema.Reference.Id, schema);

                option.OperationFilter<AddRequiredHeaderParameter>();

                option.EnableAnnotations();
            });

            services.ConfigureOptions<ConfigureSwaggerOptions>();
            return services;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            var options = app.ApplicationServices.GetService<IOptions<SwaggerOptions>>().Value;

            if (options.Disable)
                return app;

            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".yaml"] = "text/yaml";
            app.UseStaticFiles(
                new StaticFileOptions()
                {
                    ContentTypeProvider = provider
                });
            var descProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
            return app
                .UseSwagger(c =>
                {
                    c.RouteTemplate = options.DocumentRouteTemplate;
                    // if (!env.IsDevelopment())
                    {
                        //add openapiserver
                        c.PreSerializeFilters.Add((swagger, httpReq) =>
                        {
                            swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = options.OpenApiServer } };
                        });
                    }
                })
                .UseSwaggerUI(c =>
                {

                    foreach (var description in descProvider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint(options.DocumentRouteTemplate.Replace("{documentname}", description.GroupName), description.GroupName.ToUpperInvariant());
                    }

                    c.RoutePrefix = options.RoutePrefix;
                });
            
        }
    }

    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;
        private readonly IConfiguration _config;
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IConfiguration config)
        {
            this.provider = provider;
            _config = config;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // add swagger document for every API version discovered
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);

        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = _config["Swagger:Title"],
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }

}

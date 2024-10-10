namespace Baman.CrudTest.Common.Swagger
{
    public class SwaggerOptions
    {
        public const string ConfiguraionName = "Swagger";

        public string AppName { get; set; }
        public bool Disable { get; set; }
        public string DocumentRouteTemplate { get; set; }
        public string RoutePrefix { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string OpenApiServer { get; set; }
        public AuthenticationOption Authentication { get; set; }

        public struct AuthenticationOption
        {
            public string TokenUrl { get; set; }
        }
    }
}

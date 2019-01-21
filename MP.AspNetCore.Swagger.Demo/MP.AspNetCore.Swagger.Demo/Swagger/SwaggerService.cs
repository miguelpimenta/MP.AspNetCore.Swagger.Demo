using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.IO;

namespace MP.AspNetCore.Swagger.Demo.Swagger
{
    public static class SwaggerService
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SchemaFilter<SwaggerExcludePropertiesFilter>();
                //options.OperationFilter<SwaggerDefaultValues>();

                // Resolve the temporary IApiVersionDescriptionProvider service
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                // Swagger document for each API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                }

                var security = new Dictionary<string, IEnumerable<string>>
                    {
                    {"Bearer", new string[] { }},
                    };
                options.AddSecurityRequirement(security);

                // Bearer/Token
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                // Add XML document file
                SetXmlDocumentation(options);
            });
        }

        private static Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info
            {
                Title = $"Swagger Demo API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "Swagger Demo API...",
                Contact = new Contact { Name = "MP", Email = "email@somewhere.mp" },
                TermsOfService = "Bla bla bla...",
                License = new License { Name = "MIT", Url = "https://opensource.org/licenses/MIT" }
            };
            if (description.IsDeprecated)
            {
                info.Description += "\nThis Version has been deprecated.";
            }
            return info;
        }

        private static void SetXmlDocumentation(SwaggerGenOptions options)
        {
            var xmlDocumentPath = GetXmlDocumentPath();
            var existsXmlDocument = File.Exists(xmlDocumentPath);
            if (existsXmlDocument)
            {
                options.IncludeXmlComments(xmlDocumentPath);
            }
        }

        private static string GetXmlDocumentPath()
        {
            var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
            var applicationName = PlatformServices.Default.Application.ApplicationName;
            return Path.Combine(applicationBasePath, $"{applicationName}.xml");
        }
    }
}
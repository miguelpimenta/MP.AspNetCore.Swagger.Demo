using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MP.AspNetCore.Swagger.Demo.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MP.AspNetCore.Swagger.Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //!Swagger
            services.AddSwagger();

            //!Versioning
            services.AddMvcCore()
                .AddJsonFormatters()
                .AddVersionedApiExplorer(options =>
                {
                    //The format of the version added to the route URL
                    options.GroupNameFormat = "'v'VVV";
                    //Replace the version in the controller route (/api/v1/... /api/v2/...)
                    options.SubstituteApiVersionInUrl = true;
                });
            services.AddApiVersioning(options => options.ReportApiVersions = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                //Build a swagger endpoint for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    options.DocumentTitle = $"Asp.Net Core Swagger Demo API";
                    options.DocExpansion(DocExpansion.None);
                }
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
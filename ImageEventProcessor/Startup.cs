using System.Reflection;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using ImageEventProcessor.Entities;

namespace ImageEventProcessor
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Image Processing API",
                    Version = "v1",
                    Description = "A Web API for managing image events",
                    Contact = new OpenApiContact
                    {
                        Name = "Ben Richards",
                        Email = "ben@subzerodev.com"
                    }
                });

                // Add support for request examples
                options.ExampleFilters();
            });

            services.AddSwaggerExamplesFromAssemblyOf<ImageEventExample>();

            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var swaggerJsonUrl = Environment.GetEnvironmentVariable("SWAGGER_JSON_URL") 
                                 ?? "/swagger/v1/swagger.json"; 

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerJsonUrl, "Image Event API v1");
                options.RoutePrefix = string.Empty; // Swagger UI at the root
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
using EShop.Application;

namespace Api;

using Application.Contract;
using EShop.Infrastructure;
using Microsoft.OpenApi.Models;

public class Startup
{
    public IConfiguration configRoot
    {
        get;
    }
    readonly string MyAllowSpecificOrigins = "CorsPolicy";
    public Startup(IConfiguration configuration)
    {
        configRoot = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        
        services.AddInfrastructureServices(configRoot);
        services.AddApplicationServices();
        services.AddApplicationServicesContract();
        services.AddWebUIServices();
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        //services.AddSwaggerGen();


        services.AddSwaggerGen(swagger =>
        {
            //This is to generate the Default UI of Swagger Documentation
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = " Identity api",
                Description = "ASP.NET Core 5.0 Web API"
            });
            // To Enable authorization using Swagger (JWT)
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}

                    }
                });

        });
        services.AddControllers();
        services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

    }
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {

        app.UseSwagger();
        app.UseCors(MyAllowSpecificOrigins);
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V2");
        });


        app.UseRouting();
        app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        app.UseAuthentication();

        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
        
            endpoints.MapControllers(); 
            //endpoints.MapControllerRoute("default", "api/{controller=Home}/{action=Index}/{id?}");
            
        });

        //app.MapControllers();
        app.Run();
    }
}











namespace Catalog.Common.Extensions;


public static class IServiceCollectionExtension
{
    public static IServiceCollection AddServiceCollection(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(CatalogValidationBehavior<,>).Assembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CatalogValidationBehavior<,>));
        });
        services.AddEndpointsApiExplorer();
        services.AddCarter();
        services.AddExceptionHandlerMiddlewareServices();
        services.AddSwaggerConfig();
        return services;
    }

    private static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Liaro", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    private static IServiceCollection AddExceptionHandlerMiddlewareServices(this IServiceCollection services)
    {
        services.AddScoped<CatalogValidationsExceptionMiddleware>();
        return services;
    }

    public static WebApplication UseExceptionHandler(this WebApplication app)
    {
        app.UseMiddleware<CatalogValidationsExceptionMiddleware>();
        return app;
    }
}
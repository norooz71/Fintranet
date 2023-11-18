using System.Reflection;
using Application.Contract.Common.Behaviours;
using EShop.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });



        ResolveAllTypes(services, ServiceLifetime.Scoped, typeof(Service), "Service");
        

        return services;
    }

    public static void ResolveAllTypes(IServiceCollection services, ServiceLifetime serviceLifetime, Type refType, string suffix)
    {
        var assembly = refType.GetTypeInfo().Assembly;

        var allServices = assembly.GetTypes().Where(t =>
            t.GetTypeInfo().IsClass &&
            !t.GetTypeInfo().IsAbstract &&
            !t.GetType().IsInterface &&
            //(!t.Name.StartsWith("I") ||) &&
            t.Name.EndsWith(suffix)
        );


        foreach (var type in allServices)
        {
            var allInterfaces = type.GetInterfaces();
            var mainInterfaces = allInterfaces.Except
                (allInterfaces.SelectMany(t => t.GetInterfaces()));
            foreach (var itype in mainInterfaces)
            {
                if (allServices.Any(x => !x.Equals(type) && itype.IsAssignableFrom(x)))
                {
                    throw new Exception("The " + itype.Name +
                                        " type has more than one implementations, please change your filter");
                }
                services.Add(new ServiceDescriptor(itype, type, serviceLifetime));
            }
        }
    }

}

using CleanDotnetBlazor.Application.Common.Behaviours;
using CleanDotnetBlazor.Application.Common.Mappings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanDotnetBlazor.Application;
public static class DependencyInjection
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
        builder.Services.AddSingleton(mapperConfig.CreateMapper());

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });
    }
}

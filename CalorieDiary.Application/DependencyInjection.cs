using AutoMapper;
using CalorieDiary.Application.Interfaces;
using CalorieDiary.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
 
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<ICalculatorDemand, CalculatorDemandService>();

            services.AddTransient<IStartService, StartService>();
            return services;
        }
    }
}

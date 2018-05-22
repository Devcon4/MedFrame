using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace demo
{

    public static class InjectableExtensions
    {
        public static IServiceCollection AddInjectableServices(this IServiceCollection services, Assembly assembly)
        {
            var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttributes().Any(a => a.GetType() == typeof(Injectable)));
    
            foreach (var type in classTypes)
            {
                Console.WriteLine(type.Name);
                // services.AddSingleton(type);
            }
    
            return services;
        }
    }

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
            var assembly = Assembly.GetEntryAssembly();

            services.AddDbContext<DemoDB>(opt => 
                opt.UseInMemoryDatabase("DemoDB"));
            services.AddTransient<IAuthenticatedUser, AuthenticatedUser>();
            services.AddInjectableServices(assembly);
            services.AddMediatR(assembly);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

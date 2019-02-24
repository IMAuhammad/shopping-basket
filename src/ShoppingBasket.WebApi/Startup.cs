using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventFlow;
using EventFlow.Autofac.Extensions;
using EventFlow.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingBasket.Infrastructure.Basket.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Reflection;

namespace ShoppingBasket.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Info
                {
                    Title = "Shopping Basket API",
                    Version = "v1",
                    Description = "Shopping Basket RESTful API"
                });
            });

            var containerBuilder = new ContainerBuilder();

            var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Split("file:\\")[1];

            var coreTypes = Assembly.LoadFrom(rootDir + "\\ShoppingBasket.Core.dll");
            var infrastructureTypes = Assembly.LoadFrom(rootDir + "\\ShoppingBasket.Infrastructure.dll");

            var container = EventFlowOptions.New
            .UseAutofacContainerBuilder(containerBuilder)
            .AddDefaults(coreTypes)
            .AddDefaults(infrastructureTypes)
            .UseInMemoryReadStoreFor<BasketReadModel>();

            containerBuilder.Populate(services);

            return new AutofacServiceProvider(containerBuilder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopping Basket API V1");
            });
        }
    }
}

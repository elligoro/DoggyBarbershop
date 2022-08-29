using Autofac;
using DoggyBarbershop.Contracts;
using DoggyBarbershop.Helpers;
using DoggyBarbershop.Logic;
using DoggyBarbershop.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyBarbershop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionString = GetConnectionBarberShopString();
            builder.RegisterType<LoginLogic>().As<ILoginLogic>();
            builder.RegisterType<LoginRepository>()
                    .As<ILoginRepository>()
                    .WithParameter("connectionString", connectionString);

            builder.RegisterType<OrdersLogic>().As<IOrdersLogic>();
            builder.RegisterType<OrdersRepository>()
                    .As<IOrdersRepository>()
                    .WithParameter("connectionString", connectionString);
        }

        private string GetConnectionBarberShopString() => Configuration.GetConnectionString("BarberShopConnection");

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options => 
            {
                options.AddPolicy("ajaxPolicy", builder =>
                                                builder.WithOrigins("http://localhost:3000")
                                                .AllowAnyHeader()
                                                .AllowAnyMethod()
                                                .AllowCredentials()
                                                );
            });

            // handle static class
            EncryptionHandler.Initialize(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("ajaxPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}

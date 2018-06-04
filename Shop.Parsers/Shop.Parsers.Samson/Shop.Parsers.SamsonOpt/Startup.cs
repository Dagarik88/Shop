using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Parsers.SamsonOpt.Model.Configuration;
using Shop.Parsers.SamsonOpt.Service.Services.Interfaces;
using Shop.Parsers.SamsonOpt.Service.Services.Logic;

namespace Shop.Parsers.SamsonOpt
{
    public class Startup
    {
        #region Поля

        /// <summary>
        /// Конфигурация приложения.
        /// </summary>
        public IConfiguration Configuration { get; }

        #endregion Поля

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion Конструкторы

        #region Публичные методы

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppConfiguration>(Configuration);

            ConfigurateDI(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        #endregion Публичные методы

        /// <summary>
        /// Добавление зависимостей.
        /// </summary>
        /// <param name="services"></param>
        private void ConfigurateDI(IServiceCollection services)
        {
            services.AddSingleton<ISamsonApiClient, SamsonApiClient>();

            services.AddSingleton<IUpdaterManager, UpdaterManager>();

            services.AddSingleton<IHostedService, UpdaterBackgroundService>();
        }
    }
}
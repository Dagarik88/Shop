using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Data;
using Shop.Infrastructure.Repository;
using Shop.Models.Configuration;
using Shop.Services.Mappings;
using Shop.Services.Services.Interfaces;
using Shop.Services.Services.Logic;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Shop.Api
{
    public class Startup
    {
        #region Константы

        /// <summary>
        /// Имя политики CORS
        /// </summary>
        private const string CORS_POLICY_NAME = "CorsPolycy";

        #endregion Константы

        #region Поля

        /// <summary>
        /// Конфигурация приложения.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Интерфейс для получения данных окружения.
        /// </summary>
        private readonly IHostingEnvironment _env;

        #endregion Поля

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        /// <param name="env">Данные о окружении.</param>
        public Startup(
            IConfiguration configuration,
            IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        #endregion Конструкторы

        #region Публичные методы

        /// <summary>
        /// Конфигурирование сервисов.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var dbConnectionString = Configuration["DbConnectionString"] ?? Configuration.GetConnectionString("DefaultConnection");

            services.AddScoped(typeof(ICategoryManager), typeof(CategoryManager));

            // конфигурирование БД
            ConfigureDatabaseService(services, dbConnectionString);

            // конфигурирование .Core и MVC
            ConfigureMvcNetCoreService(services);

            // конфигурирование AutoMapper
            ConfigureAutoMapper(services);

            // конфигурирование Swagger
            ConfigureSwaggerService(services);

            // считываем конфигурацию приложения в объект
            services.Configure<AppConfiguration>(Configuration);

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Метод конфигурирующий конвеер запросов.
        /// </summary>
        /// <param name="env">Данные о окружении.</param>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var dbContext = app.ApplicationServices.GetService<ShopContext>();

            dbContext.Database.Migrate();

            // настройка политики Cors
            app.UseCors(CORS_POLICY_NAME);

            // настройка документирования (Swagger)
            AddSwaggerMiddleware(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }

        #endregion Публичные методы

        #region Приватные методы (Services)

        /// <summary>
        /// Настройка работы с БД.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <param name="dbConnectionString">Строка подключения к БД.</param>
        private void ConfigureDatabaseService(IServiceCollection services, string dbConnectionString)
        {
            services.AddUnitOfWork<ShopContext>();

            services.AddEntityFrameworkNpgsql()
                    .AddDbContext<ShopContext>(options =>
                    {
                        options.UseNpgsql(dbConnectionString,
                            sqlOptions =>
                            {
                                sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                sqlOptions.EnableRetryOnFailure(
                                    maxRetryCount: 5,
                                    maxRetryDelay: TimeSpan.FromSeconds(30),
                                    errorCodesToAdd: null);
                            });
                    },
                        ServiceLifetime.Scoped
                    );
        }

        /// <summary>
        /// Основное конфигурирование .Core и Mvc.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        private void ConfigureMvcNetCoreService(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy(CORS_POLICY_NAME,
                    builder => builder
                               .AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials());
            });

            services
                .AddMvcCore()
                .AddJsonFormatters()
                .AddAuthorization()
                .AddDataAnnotations()
                .AddApiExplorer();
        }

        /// <summary>
        /// Настройка AutoMapper.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        private void ConfigureAutoMapper(IServiceCollection services)
        {
            Mapper.Initialize(expression => expression.AddProfiles(AutoMapperConfiguration.GetProfiles()));
            services.AddSingleton(Mapper.Instance);
        }

        /// <summary>
        /// Настройка документирования (Swagger)
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        private void ConfigureSwaggerService(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();

                options.SwaggerDoc("Shop.Api", new Info
                {
                    Title = "Shop API",
                    Version = "ShopApi v.1",
                    Description = "Описание API методов",
                    TermsOfService = ""
                });

                Array.ForEach(new[] { "Shop.Api.xml", "Shop.Data.xml", "Shop.Models.xml" }, xml =>
                {
                    var xmlPath = Path.Combine(_env.ContentRootPath, xml);
                    options.IncludeXmlComments(xmlPath);
                });

                options.SwaggerDoc("v1", new Info
                {
                    Title = "Shop Api",
                    Version = "v1",
                    Description = "",
                    TermsOfService = ""
                });
            });
        }

        #endregion Приватные методы (Services)

        #region Приватные методы (Middleware)

        /// <summary>
        /// Настройка документирования (Swagger).
        /// </summary>
        private void AddSwaggerMiddleware(IApplicationBuilder app)
        {
            var basePath = Configuration.GetValue<string>("BasePath") ?? "";

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.RoutePrefix = "api-docs";
                   c.SwaggerEndpoint($"{basePath}/swagger/Shop.Api/swagger.json", "ShopApi");
               });
        }

        #endregion Приватные методы (Middleware)
    }
}
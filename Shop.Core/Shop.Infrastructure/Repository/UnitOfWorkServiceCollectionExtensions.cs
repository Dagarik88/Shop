using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Infrastructure.Repository.Interfaces;

namespace Shop.Infrastructure.Repository
{
    /// <summary>
    /// Расширение для регистрации единицы работы с БД как службы в <see cref="IServiceCollection"/>.
    /// </summary>
    public static class UnitOfWorkServiceCollectionExtensions
    {
        /// <summary>
        /// Регистрирует единицу работы данного контекста как службу в <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TContext">Тип контекста работы в БД.</typeparam>
        /// <param name="services">Сервис <see cref="IServiceCollection"/>.</param>
        /// <returns>Сервис с подключенной службой.</returns>
        /// <remarks>
        /// Этот метод поддерживает только один контекст БД, если был вызван несколько раз, вызовет исключение.
        /// </remarks>
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddScoped<IRepositoryFactory, UnitOfWork<TContext>>();
            // Ниже есть проблема: IUnitOfWork не может поддерживать несколько коннектсов БД
            // невозможно вызвать AddUnitOfWork<TContext> несколько раз.
            // Solution: проверить IUnitOfWork или null
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();

            return services;
        }

        /// <summary>
        /// Регистрирует единицы работы данного контекста как службу в <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TContext1">Тип контекста работы в БД.</typeparam>
        /// <typeparam name="TContext2">Тип контекста работы в БД.</typeparam>
        /// <param name="services">Сервис <see cref="IServiceCollection"/>.</param>
        /// <returns>Сервис с подключенной службой.</returns>
        /// <remarks>
        /// Этот метод поддерживает только один контекст БД, если был вызван несколько раз, вызовет исключение.
        /// </remarks>
        public static IServiceCollection AddUnitOfWork<TContext1, TContext2>(this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
        {
            services.AddScoped<IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();

            return services;
        }

        /// <summary>
        /// Регистрирует единицы работы данного контекста как службу в <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TContext1">Тип контекста работы в БД.</typeparam>
        /// <typeparam name="TContext2">Тип контекста работы в БД.</typeparam>
        /// <typeparam name="TContext3">Тип контекста работы в БД.</typeparam>
        /// <param name="services">Сервис <see cref="IServiceCollection"/>.</param>
        /// <returns>Сервис с подключенной службой.</returns>
        /// <remarks>
        /// Этот метод поддерживает только один контекст БД, если был вызван несколько раз, вызовет исключение.
        /// </remarks>
        public static IServiceCollection AddUnitOfWork<TContext1, TContext2, TContext3>(this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
            where TContext3 : DbContext
        {
            services.AddScoped<IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();
            services.AddScoped<IUnitOfWork<TContext3>, UnitOfWork<TContext3>>();

            return services;
        }
    }
}
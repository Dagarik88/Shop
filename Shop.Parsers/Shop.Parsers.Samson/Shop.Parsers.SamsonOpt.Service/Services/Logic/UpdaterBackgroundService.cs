using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shop.Infrastructure.BackgroundService;
using Shop.Parsers.SamsonOpt.Model.Configuration;
using Shop.Parsers.SamsonOpt.Model.Configuration.Enums;
using Shop.Parsers.SamsonOpt.Service.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Parsers.SamsonOpt.Service.Services.Logic
{
    /// <summary>
    /// Background служба обновления.
    /// </summary>
    public class UpdaterBackgroundService : BaseBackgroundService
    {
        #region Поля

        /// <summary>
        /// Конфигурация приложения.
        /// </summary>
        private readonly AppConfiguration _configuration;

        /// <summary>
        /// Провайдер для получения зависимостей.
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        #endregion Поля

        #region Конструктор

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        /// <param name="serviceProvider">Провайдер для получения зависимостей.</param>
        public UpdaterBackgroundService(
            IOptions<AppConfiguration> configuration,
            IServiceProvider serviceProvider)
        {
            _configuration = configuration.Value;
            _serviceProvider = serviceProvider;
        }

        #endregion Конструктор

        #region Публичные методы

        /// <summary>
        /// Основной метод периодичного запуска сервиса обновлений.
        /// </summary>
        /// <param name="stoppingToken">Маркер отмены.</param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //
            if (_configuration.UpdateTimeType.Equals(UpdateTimeType.None))
            {
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var serviceScope = _serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var updaterManager = serviceScope.ServiceProvider.GetRequiredService<IUpdaterManager>();

                    await updaterManager.Run();
                }

                await Task.Delay(GetDelay(), stoppingToken);
            }
        }

        #endregion Публичные методы

        #region Приватные методы

        private TimeSpan GetDelay()
        {
            var result = default(TimeSpan);
            var checkUdateTime = _configuration.CheckUpdateTime;

            switch (_configuration.UpdateTimeType)
            {
                case UpdateTimeType.Second:
                    result = TimeSpan.FromSeconds(checkUdateTime);
                    break;

                case UpdateTimeType.Minute:
                    result = TimeSpan.FromMinutes(checkUdateTime);
                    break;

                case UpdateTimeType.Hour:
                    result = TimeSpan.FromHours(checkUdateTime);
                    break;

                case UpdateTimeType.Day:
                    result = TimeSpan.FromDays(checkUdateTime);
                    break;

                default:
                    result = TimeSpan.MinValue;
                    break;
            }
            return result;
        }

        #endregion Приватные методы
    }
}
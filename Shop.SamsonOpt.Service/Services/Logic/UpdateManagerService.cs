using Microsoft.Extensions.Options;
using Shop.Infrastructure.Services;
using Shop.SamsonOpt.Model.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.SamsonOpt.Service.Services.Logic
{
    /// <summary>
    /// Background сервис обновления данных.
    /// </summary>
    public class UpdateManagerService : BackgroundService
    {
        #region Поля

        /// <summary>
        /// Конфигурация приложения.
        /// </summary>
        private readonly AppConfiguration _configuration;

        #endregion Поля

        #region Конструктор

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        public UpdateManagerService(
            IOptions<AppConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        #endregion Конструктор

        #region Методы

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_configuration.RequestPeriod, stoppingToken);
            }
        }

        #endregion Методы
    }
}

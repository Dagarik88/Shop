using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.BackgroundService
{
    /// <summary>
    /// Базовый класс Background службы.
    /// </summary>
    public abstract class BaseBackgroundService : IHostedService, IDisposable
    {
        #region Поля

        /// <summary>
        /// Выполняемая задача.
        /// </summary>
        private Task _executingTask;

        /// <summary>
        ///
        /// </summary>
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        #endregion Поля

        #region Публичные методы

        /// <summary>
        /// Запустим задачу.
        /// </summary>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Задачу.</returns>
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            // сохраним запускаемую задачу
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            // Если задача завершена вернём её
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            // в противном случае она всё ещё работает
            return Task.CompletedTask;
        }

        /// <summary>
        /// Завершаем задачу.
        /// </summary>
        /// <param name="cancellationToken">Маркер отмены.</param>
        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            // Завершение без вызова
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                // Отмечаем что задача завершена
                _stoppingCts.Cancel();
            }
            finally
            {
                // Ожидаем завершения по времени или по токену
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                                                              cancellationToken));
            }
        }

        /// <summary>
        /// Уничтожить объект с задачей.
        /// </summary>
        public virtual void Dispose()
        {
            _stoppingCts.Cancel();
        }

        #endregion Публичные методы

        /// <summary>
        /// Логика выполнения задачи.
        /// </summary>
        /// <param name="stoppingToken">Маркер отмены.</param>
        protected abstract Task ExecuteAsync(CancellationToken stoppingToken);
    }
}
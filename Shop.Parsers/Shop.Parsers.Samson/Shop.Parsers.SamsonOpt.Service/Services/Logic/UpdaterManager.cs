using Shop.Parsers.SamsonOpt.Service.Services.Interfaces;
using System.Threading.Tasks;

namespace Shop.Parsers.SamsonOpt.Service.Services.Logic
{
    /// <summary>
    /// Служба обновления данных.
    /// </summary>
    public class UpdaterManager : IUpdaterManager
    {
        #region Поля

        /// <summary>
        /// HTTP клиент к API сервису Самсона.
        /// </summary>
        private readonly ISamsonApiClient _samsonApiClient;

        #endregion Поля

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public UpdaterManager(
            ISamsonApiClient samsonApiClient)
        {
            _samsonApiClient = samsonApiClient;
        }

        #endregion Конструкторы

        #region Публичные методы

        /// <summary>
        /// Запуск службы.
        /// </summary>
        public async Task Run()
        {
            var category = await _samsonApiClient.GetCategory();

            var assortments = await _samsonApiClient.GetAssortment();
        }

        #endregion Публичные методы
    }
}
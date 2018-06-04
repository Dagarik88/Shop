using System.Threading.Tasks;

namespace Shop.Parsers.SamsonOpt.Service.Services.Interfaces
{
    /// <summary>
    /// Интерфейс службы обновления данных.
    /// </summary>
    public interface IUpdaterManager
    {
        /// <summary>
        /// Запуск службы.
        /// </summary>
        Task Run();
    }
}
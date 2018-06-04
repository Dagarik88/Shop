using Shop.Parsers.SamsonOpt.Model.ApiClientModels.Data.Assortment;
using Shop.Parsers.SamsonOpt.Model.ApiClientModels.Data.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Parsers.SamsonOpt.Service.Services.Interfaces
{
    /// <summary>
    /// Интерфейс HTTP клиента к API сервисам Самсона.
    /// </summary>
    public interface ISamsonApiClient
    {
        /// <summary>
        /// Возвращает список категорий.
        /// </summary>
        /// <returns>Список категорий.</returns>
        Task<IEnumerable<CategoryModel>> GetCategory();

        /// <summary>
        /// Возвращает список продуктов.
        /// </summary>
        /// <returns>Список продуктов.</returns>
        Task<IEnumerable<AssortmentModel>> GetAssortment();
    }
}
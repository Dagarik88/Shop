using Shop.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Services.Services.Interfaces
{
    /// <summary>
    /// Итерфейс сервиса по управлению категориями каталога.
    /// </summary>
    public interface ICategoryManager
    {
        /// <summary>
        /// Возвращает категорию каталога.
        /// </summary>
        /// <param name="categoryId">ID значение категории.</param>
        /// <returns>Категория каталога <see cref="Category"/>.</returns>
        Task<Category> GetCategory(int categoryId);

        /// <summary>
        /// Возвращает все категории каталога.
        /// </summary>
        /// <returns>Список категорий каталога.</returns>
        Task<IEnumerable<Category>> GetCategories();

        /// <summary>
        /// Удаляет категорию каталога.
        /// </summary>
        /// <param name="categoryId">ID значение категории.</param>
        /// <returns>Категория каталога <see cref="Category"/>.</returns>
        Task<bool> DeleteCategory(int categoryId);
    }
}
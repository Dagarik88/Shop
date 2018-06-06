using Shop.Data.Common;
using Shop.Infrastructure.Repository.Interfaces;
using Shop.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Services.Services.Logic
{
    /// <summary>
    /// Сервис по управлению категориями каталога.
    /// </summary>
    public class CategoryManager : ICategoryManager
    {
        #region Поля

        /// <summary>
        /// Репозиторий категорий каталога.
        /// </summary>
        private readonly IRepository<Category> _categoryRepository;

        /// <summary>
        /// Единица работы с БД.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        #endregion Поля

        #region Конструктор

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="unitOfWork">Единица работы с БД.</param>
        public CategoryManager(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _categoryRepository = _unitOfWork.GetRepository<Category>();
        }

        #endregion Конструктор

        #region Публичные методы

        /// <summary>
        /// Возвращает категорию каталога.
        /// </summary>
        /// <param name="categoryId">ID значение категории.</param>
        /// <returns>Категория каталога <see cref="Category"/>.</returns>
        public async Task<Category> GetCategory(int categoryId)
        {
            var category = default(Category);

            category = await _categoryRepository.GetFirstOrDefaultAsync(predicate: c => c.Id == categoryId);

            return category;
        }

        /// <summary>
        /// Возвращает все категории каталога.
        /// </summary>
        /// <returns>Список категорий каталога.</returns>
        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = default(IEnumerable<Category>);

            categories = await _categoryRepository.GetListAsync(predicate: c => c.IsActive);

            return categories;
        }

        /// <summary>
        /// Удаляет категорию каталога.
        /// </summary>
        /// <param name="categoryId">ID значение категории.</param>
        /// <returns>Категория каталога <see cref="Category"/>.</returns>
        public async Task<bool> DeleteCategory(int categoryId)
        {
            try
            {
                await Task.Run(() =>
                {
                    _categoryRepository.Delete(categoryId);
                });
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        #endregion Публичные методы
    }
}
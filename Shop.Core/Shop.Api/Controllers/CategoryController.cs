using Microsoft.AspNetCore.Mvc;
using Shop.Data.Common;
using Shop.Services.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    /// <summary>
    /// Контроллер по управлению категориями каталога.
    /// </summary>
    [Route("/api/categories")]
    public class CategoryController : Controller
    {
        #region Поля

        /// <summary>
        /// Сервис по управлению категориями каталога.
        /// </summary>
        private readonly ICategoryManager _categoryManager;

        #endregion Поля

        #region Конструктор

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="categoryManager">Сервис по управлению категориями каталога.</param>
        public CategoryController(
            ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        #endregion Конструктор

        #region API Методы

        /// <summary>
        /// Возвращает список всех категорий каталога.
        /// </summary>
        /// <returns>Список всех категорий каталога.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> GetCategories()
        {
            var result = await _categoryManager.GetCategories();

            return result;
        }

        /// <summary>
        /// Добавляет категорию в каталог.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> AddCategory(int? parentId, string name, int? order)
        {
            return "";
        }

        /// <summary>
        /// Массовое обновление категорий в каталоге.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> UpdateCategories()
        {
            return "";
        }

        /// <summary>
        /// Массовое обновление категорий в каталоге.
        /// </summary>
        /// <param name="categoryIds">Массив ID значений категорий.</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> DeleteCategories(int[] categoryIds)
        {
            return "";
        }

        /// <summary>
        /// Возвращает категорию каталога с подкатегориями.
        /// </summary>
        /// <param name="categoryId">ID значение категории.</param>
        /// <returns>Категорию каталога с подкатегориями.</returns>
        [HttpGet("{categoryId}")]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> GetCategory(int categoryId)
        {
            var result = await _categoryManager.GetCategory(categoryId);

            if (result == null)
            {
                return new NotFoundResult();
            }

            return result;
        }

        /// <summary>
        /// Обновляет категорию в каталоге.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{categoryId}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> UpdateCategory(int categoryId, string name, int? order, int? parentId, bool isActive = true)
        {
            return "";
        }

        /// <summary>
        /// Удаляет категорию из каталога.
        /// </summary>
        /// <param name="categoryId">ID значение категории.</param>
        /// <returns></returns>
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> DeleteCategory(int categoryId)
        {
            var result = await _categoryManager.DeleteCategory(categoryId);
            if (result)
            {
                return new OkObjectResult(result);
            }

            return new BadRequestObjectResult(result);
        }

        #endregion API Методы
    }
}
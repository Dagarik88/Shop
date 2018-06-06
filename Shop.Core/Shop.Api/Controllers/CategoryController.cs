using Microsoft.AspNetCore.Mvc;
using Shop.Data.Common;
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
        #region Конструктор

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public CategoryController()
        {
        }

        #endregion Конструктор

        #region API Методы

        /// <summary>
        /// Возвращает список всех категории каталога.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> GetCategories()
        {
            return "";
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
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> DeleteCategories(int[] categoryIds)
        {
            return "";
        }

        /// <summary>
        /// Возвращает категорию каталога со всеми дочерними связями.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{categoryId}")]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> GetCategory(int categoryId)
        {
            return "";
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
        /// <returns></returns>
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> DeleteCategory(int categoryId)
        {
            return "";
        }

        #endregion API Методы
    }
}
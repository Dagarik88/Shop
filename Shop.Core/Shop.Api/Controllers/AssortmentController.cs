using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    /// <summary>
    /// Контроллер по управлению продуктами каталога.
    /// </summary>
    [Route("/api/categories/{categoryId}/assortments")]
    public class AssortmentController : Controller
    {
        #region Конструктор

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public AssortmentController()
        {
        }

        #endregion Конструктор

        #region API Методы

        /// <summary>
        /// Возвращает список всех продуктов из заданного каталога.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> GetAssortments(int categoryId)
        {
            return "";
        }

        /// <summary>
        /// Добавляет продукт в каталог.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> AddAssortment(int categoryId)
        {
            return "";
        }

        /// <summary>
        /// Обновляет массово продукты.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> UpdateAssortments(int categoryId)
        {
            return "";
        }

        /// <summary>
        /// Удаляет массово продукты.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> DeleteAssortments(int categoryId)
        {
            return "";
        }

        /// <summary>
        /// Возвращает продукт.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{assortmentId}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> GetAssortment(int categoryId, int assortmentId)
        {
            return "";
        }

        /// <summary>
        /// Обновляет продукт в каталоге.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{assortmentId}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> UpdateAssortment(int categoryId, int assortmentId)
        {
            return "";
        }

        /// <summary>
        /// Удаляет продукт из каталога.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{assortmentId}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<object> DeleteAssortment(int categoryId, int assortmentId)
        {
            return "";
        }

        #endregion API Методы
    }
}
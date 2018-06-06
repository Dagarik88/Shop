using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Shop.Api.Controllers
{
    /// <summary>
    /// Контроллер главной страницы.
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Инициирует контроллер.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Перенаправляет на страницу в документацией.
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            var basePath = _configuration.GetValue<string>("BasePath") ?? "";

            return new RedirectResult($"~{basePath}/api-docs");
        }
    }
}
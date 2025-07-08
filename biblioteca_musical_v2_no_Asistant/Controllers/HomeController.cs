using System.Diagnostics;
using biblioteca_musical_v2_no_Asistant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace biblioteca_musical_v2_no_Asistant.Controllers
{
    /// <summary>
    /// Controlador principal que maneja las vistas Home: Index, Privacy y Error.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor que recibe el logger por inyección de dependencias.
        /// </summary>
        /// <param name="logger">Instancia de ILogger para registrar eventos y errores.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// GET: /Home/Index
        /// Muestra la vista principal de la aplicación.
        /// </summary>
        /// <returns>Vista Index.</returns>
        public IActionResult Index()
        {
            // Retorna la vista Index (página de inicio)
            return View();
        }

        /// <summary>
        /// GET: /Home/Privacy
        /// Muestra la política de privacidad de la aplicación.
        /// </summary>
        /// <returns>Vista Privacy.</returns>
        public IActionResult Privacy()
        {
            // Retorna la vista Privacy (política de privacidad)
            return View();
        }

        /// <summary>
        /// GET: /Home/Error
        /// Muestra la vista de error con información de diagnóstico.
        /// </summary>
        /// <returns>Vista Error con el modelo ErrorViewModel.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Crea el modelo de error con el RequestId actual o identificador de trazado
            var errorModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            // Retorna la vista Error pasándole el modelo de error
            return View(errorModel);
        }
    }
}

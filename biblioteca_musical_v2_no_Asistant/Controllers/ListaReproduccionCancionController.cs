using biblioteca_musical_v2_no_Asistant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace biblioteca_musical_v2_no_Asistant.Controllers
{
    /// <summary>
    /// Controlador para gestionar las relaciones entre Listas de Reproducción y Canciones (CRUD).
    /// </summary>
    public class ListaReproduccionCancionController : Controller
    {
        private readonly BibliotecaMusicalContext _db;

        /// <summary>
        /// Constructor que inicializa el controlador con el contexto de datos.
        /// </summary>
        /// <param name="db">Contexto de EF Core para acceder a la base de datos.</param>
        public ListaReproduccionCancionController(BibliotecaMusicalContext db)
        {
            _db = db;
        }

        /// <summary>
        /// GET: /ListaReproduccionCancion/Index
        /// Recupera todas las relaciones Lista–Canción incluyendo detalles y las muestra.
        /// </summary>
        /// <returns>Vista con la lista de relaciones (<see cref="ListaReproduccionCancion"/>).</returns>
        public async Task<IActionResult> Index()
        {
            // Incluye las entidades relacionadas para mostrar nombre de lista y título de canción
            var list = await _db.ListaCanciones
                                .Include(x => x.Lista)
                                .Include(x => x.Cancion)
                                .ToListAsync();

            // Prepara SelectList para dropdowns en creación/edición
            ViewBag.Listas = new SelectList(
                await _db.Listas.ToListAsync(), "IdLista", "Nombre");
            ViewBag.Canciones = new SelectList(
                await _db.Canciones.ToListAsync(), "IdCancion", "Titulo");

            return View(list);
        }

        /// <summary>
        /// GET: /ListaReproduccionCancion/Create
        /// Muestra el formulario en blanco para crear una nueva relación Lista–Canción.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Configura SelectList para dropdowns
            ViewBag.Listas = new SelectList(
                await _db.Listas.ToListAsync(), "IdLista", "Nombre");
            ViewBag.Canciones = new SelectList(
                await _db.Canciones.ToListAsync(), "IdCancion", "Titulo");

            return View();
        }

        /// <summary>
        /// POST: /ListaReproduccionCancion/Create
        /// Crea una nueva relación Lista–Canción.
        /// </summary>
        /// <param name="model">Datos de la nueva relación.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ListaReproduccionCancion model)
        {
            if (!ModelState.IsValid)
            {
                // Validación incorrecta: recarga dropdowns y vuelve a mostrar formulario
                ViewBag.Listas = new SelectList(
                    await _db.Listas.ToListAsync(), "IdLista", "Nombre", model.IdLista);
                ViewBag.Canciones = new SelectList(
                    await _db.Canciones.ToListAsync(), "IdCancion", "Titulo", model.IdCancion);

                return View(model);
            }

            _db.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// GET: /ListaReproduccionCancion/Edit/{id}
        /// Muestra el formulario de edición para una relación existente.
        /// </summary>
        /// <param name="id">ID de la relación a editar.</param>
        /// <returns>Vista Edit con el modelo cargado.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _db.ListaCanciones.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            // Configura SelectList con la opción seleccionada
            ViewBag.Listas = new SelectList(
                await _db.Listas.ToListAsync(), "IdLista", "Nombre", entity.IdLista);
            ViewBag.Canciones = new SelectList(
                await _db.Canciones.ToListAsync(), "IdCancion", "Titulo", entity.IdCancion);

            return View(entity);
        }

        /// <summary>
        /// POST: /ListaReproduccionCancion/Edit/{id}
        /// Actualiza los datos de una relación Lista–Canción.
        /// </summary>
        /// <param name="id">ID de la relación a actualizar.</param>
        /// <param name="model">Modelo con los datos modificados.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ListaReproduccionCancion model)
        {
            if (id != model.IdListaRepCan || !ModelState.IsValid)
            {
                // ID no coincide o validación fallida
                return RedirectToAction(nameof(Index));
            }

            var entity = await _db.ListaCanciones.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            // Actualiza únicamente los campos permisibles
            entity.IdLista = model.IdLista;
            entity.IdCancion = model.IdCancion;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// POST: /ListaReproduccionCancion/Delete/{id}
        /// Elimina una relación Lista–Canción.
        /// </summary>
        /// <param name="id">ID de la relación a eliminar.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _db.ListaCanciones.FindAsync(id);
            if (entity != null)
            {
                _db.ListaCanciones.Remove(entity);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

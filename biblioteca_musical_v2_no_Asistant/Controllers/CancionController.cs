using biblioteca_musical_v2_no_Asistant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace biblioteca_musical_v2_no_Asistant.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones CRUD de la entidad <see cref="Cancion"/>.
    /// </summary>
    public class CancionController : Controller
    {
        private readonly BibliotecaMusicalContext _db;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos por inyección de dependencias.
        /// </summary>
        /// <param name="db">Contexto de EF Core para acceder a la base de datos.</param>
        public CancionController(BibliotecaMusicalContext db)
        {
            _db = db;
        }

        /// <summary>
        /// GET: /Cancion/Index
        /// Recupera todas las canciones activas, incluyendo datos de artista y género, y las muestra en la vista.
        /// </summary>
        /// <returns>Vista con la lista de <see cref="Cancion"/>.</returns>
        public async Task<IActionResult> Index()
        {
            // Incluye las relaciones para mostrar nombre de artista y género
            var list = await _db.Canciones
                                .Where(c => c.Estado == "Activo")
                                .Include(c => c.IdArtistaNavigation)
                                .Include(c => c.IdGeneroNavigation)
                                .ToListAsync();

            // Prepara los SelectList para el formulario de creación y para los filtros
            ViewBag.Artistas = new SelectList(
                await _db.Artistas.ToListAsync(), "IdArtista", "Nombre");
            ViewBag.Generos = new SelectList(
                await _db.Generos.ToListAsync(), "IdGenero", "Nombre");

            return View(list);
        }

        /// <summary>
        /// GET: /Cancion/Create
        /// Muestra el formulario en blanco para crear una nueva canción.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Inicializa los SelectList para dropdowns en la vista
            ViewBag.Artistas = new SelectList(
                await _db.Artistas.ToListAsync(), "IdArtista", "Nombre");
            ViewBag.Generos = new SelectList(
                await _db.Generos.ToListAsync(), "IdGenero", "Nombre");
            return View();
        }

        /// <summary>
        /// POST: /Cancion/Create
        /// Crea una nueva canción en la base de datos.
        /// </summary>
        /// <param name="m">Modelo de <see cref="Cancion"/> con los datos del formulario.</param>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cancion m)
        {
            if (!ModelState.IsValid)
            {
                // Validación fallida: volver a mostrar formulario con errores
                ViewBag.Artistas = new SelectList(
                    await _db.Artistas.ToListAsync(), "IdArtista", "Nombre", m.IdArtista);
                ViewBag.Generos = new SelectList(
                    await _db.Generos.ToListAsync(), "IdGenero", "Nombre", m.IdGenero);
                return View(m);
            }

            // Asignación de auditoría
            m.FechaCreacion = DateTime.UtcNow;
            m.FechaModificacion = DateTime.UtcNow;

            _db.Add(m);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// GET: /Cancion/Edit/{id}
        /// Muestra el formulario de edición para la canción especificada.
        /// </summary>
        /// <param name="id">Identificador de la canción a editar.</param>
        /// <returns>Vista de edición con el modelo <see cref="Cancion"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Busca la entidad por su PK
            var entity = await _db.Canciones.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            // Prepara select lists con la opción seleccionada
            ViewBag.Artistas = new SelectList(
                await _db.Artistas.ToListAsync(), "IdArtista", "Nombre", entity.IdArtista);
            ViewBag.Generos = new SelectList(
                await _db.Generos.ToListAsync(), "IdGenero", "Nombre", entity.IdGenero);

            return View(entity);
        }

        /// <summary>
        /// POST: /Cancion/Edit/{id}
        /// Aplica los cambios al modelo <see cref="Cancion"/> en la base de datos.
        /// </summary>
        /// <param name="id">Identificador de la canción a actualizar.</param>
        /// <param name="m">Modelo con los nuevos datos.</param>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cancion m)
        {
            if (id != m.IdCancion || !ModelState.IsValid)
            {
                // ID mismatch o validación fallida
                return RedirectToAction(nameof(Index));
            }

            // Busca la entidad original
            var e = await _db.Canciones.FindAsync(id);
            if (e == null)
            {
                return NotFound();
            }

            // Actualiza únicamente los campos modificables
            e.Titulo = m.Titulo;
            e.DuracionCancion = m.DuracionCancion;
            e.IdArtista = m.IdArtista;
            e.IdGenero = m.IdGenero;
            e.AnioLanzamiento = m.AnioLanzamiento;
            e.Estado = m.Estado;
            e.FechaModificacion = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// POST: /Cancion/Delete/{id}
        /// Realiza un "soft delete" de la canción especificada. Si ocurre una excepción por restricciones de integridad referencial,
        /// se notifica al usuario mediante TempData.
        /// </summary>
        /// <param name="id">Identificador de la canción a borrar.</param>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _db.Canciones.FindAsync(id);
            if (e != null)
            {
                try
                {
                    e.Estado = "Inactivo";
                    e.FechaModificacion = DateTime.UtcNow;
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    TempData["Error"] = "No se puede eliminar esta canción porque está relacionada con otras entidades.";
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

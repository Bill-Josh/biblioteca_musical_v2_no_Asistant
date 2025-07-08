using biblioteca_musical_v2_no_Asistant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace biblioteca_musical_v2_no_Asistant.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones CRUD de la entidad <see cref="Artista"/>.
    /// </summary>
    public class ArtistaController : Controller
    {
        private readonly BibliotecaMusicalContext _db;

        /// <summary>
        /// Constructor que inicializa el controlador con el contexto de base de datos.
        /// </summary>
        /// <param name="db">Contexto de EF Core para acceder a los datos.</param>
        public ArtistaController(BibliotecaMusicalContext db)
        {
            _db = db;
        }

        /// <summary>
        /// GET: /Artista/Index
        /// Muestra la lista de artistas.
        /// </summary>
        /// <returns>Vista con listado de <see cref="Artista"/>.</returns>
        public async Task<IActionResult> Index()
        {
            // Obtiene todos los artistas de la base de datos
            var lista = await _db.Artistas.ToListAsync();
            return View(lista);
        }

        /// <summary>
        /// GET: /Artista/Create
        /// Muestra el formulario en blanco para crear un nuevo Artista.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: /Artista/Create
        /// Crea un nuevo artista.
        /// </summary>
        /// <param name="model">Datos del artista a crear.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Artista model)
        {
            if (!ModelState.IsValid)
            {
                // Si la validación falla, devolvemos la vista con los errores
                return View(model);
            }

            // Asigna fechas de auditoría
            model.FechaCreacion = DateTime.UtcNow;
            model.FechaModificacion = DateTime.UtcNow;

            // Guarda el nuevo artista
            _db.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// GET: /Artista/Edit/{id}
        /// Muestra el formulario de edición para el artista especificado.
        /// </summary>
        /// <param name="id">Identificador del artista a editar.</param>
        /// <returns>Vista de edición con datos del artista.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Busca el artista por su clave primaria
            var entity = await _db.Artistas.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            // Devuelve la vista Edit con el modelo
            return View(entity);
        }

        /// <summary>
        /// POST: /Artista/Edit/{id}
        /// Aplica los cambios al artista en la base de datos.
        /// </summary>
        /// <param name="id">Identificador del artista a actualizar.</param>
        /// <param name="model">Modelo con los nuevos datos.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Artista model)
        {
            if (id != model.IdArtista || !ModelState.IsValid)
            {
                // Si hay error en la validación o el id no coincide
                return RedirectToAction(nameof(Index));
            }

            // Obtiene la entidad existente
            var entity = await _db.Artistas.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            // Actualiza solo los campos permitidos
            entity.Nombre = model.Nombre;
            entity.Apellido = model.Apellido;
            entity.NombreArtistico = model.NombreArtistico;
            entity.TipoArtista = model.TipoArtista;
            entity.PaisOrigen = model.PaisOrigen;
            entity.Biografia = model.Biografia;
            entity.FechaModificacion = DateTime.UtcNow;

            // Guarda los cambios
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// POST: /Artista/Delete/{id}
        /// Elimina un artista de la base de datos.
        /// </summary>
        /// <param name="id">Identificador del artista a eliminar.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _db.Artistas.FindAsync(id);
            if (entity != null)
            {
                // Elimina y guarda cambios
                _db.Artistas.Remove(entity);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

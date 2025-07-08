using System;
using biblioteca_musical_v2_no_Asistant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace biblioteca_musical_v2_no_Asistant.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones CRUD de la entidad <see cref="ListaReproduccion"/>.
    /// </summary>
    public class ListaReproduccionController : Controller
    {
        private readonly BibliotecaMusicalContext _db;

        /// <summary>
        /// Constructor que inicializa el controlador con el contexto de datos.
        /// </summary>
        /// <param name="db">Contexto de EF Core para acceder a la base de datos.</param>
        public ListaReproduccionController(BibliotecaMusicalContext db)
        {
            _db = db;
        }

        /// <summary>
        /// GET: /ListaReproduccion/Index
        /// Recupera todas las listas de reproducción y las muestra en la vista.
        /// </summary>
        /// <returns>Vista con la lista de entidades <see cref="ListaReproduccion"/>.</returns>
        public async Task<IActionResult> Index()
        {
            var list = await _db.Listas.ToListAsync();
            return View(list);
        }

        /// <summary>
        /// GET: /ListaReproduccion/Create
        /// Muestra el formulario en blanco para crear una nueva lista de reproducción.
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: /ListaReproduccion/Create
        /// Crea una nueva lista de reproducción.
        /// </summary>
        /// <param name="model">Modelo de <see cref="ListaReproduccion"/> con los datos del formulario.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ListaReproduccion model)
        {
            if (!ModelState.IsValid)
            {
                // Si la validación falla, volvemos a mostrar el formulario con errores
                return View(model);
            }

            // Asignar fechas de auditoría
            model.FechaCreacion = DateTime.UtcNow;
            model.FechaModificacion = DateTime.UtcNow;

            _db.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// GET: /ListaReproduccion/Edit/{id}
        /// Muestra el formulario de edición para la lista seleccionada.
        /// </summary>
        /// <param name="id">Identificador de la lista a editar.</param>
        /// <returns>Vista de edición con el modelo cargado.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _db.Listas.FindAsync(id);
            if (entity == null)
            {
                // Si no existe la entidad, devuelve 404
                return NotFound();
            }

            return View(entity);
        }

        /// <summary>
        /// POST: /ListaReproduccion/Edit/{id}
        /// Actualiza los datos de una lista de reproducción existente.
        /// </summary>
        /// <param name="id">Identificador de la lista a actualizar.</param>
        /// <param name="model">Modelo con los datos modificados.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ListaReproduccion model)
        {
            if (id != model.IdLista || !ModelState.IsValid)
            {
                // Si hay desajuste de ID o validación fallida, vuelve al índice
                return RedirectToAction(nameof(Index));
            }

            var entity = await _db.Listas.FindAsync(id);
            if (entity == null)
            {
                // Si no se encuentra la entidad, devuelve 404
                return NotFound();
            }

            // Actualizar campos modificables
            entity.Nombre = model.Nombre;
            entity.Descripcion = model.Descripcion;
            entity.FechaModificacion = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// POST: /ListaReproduccion/Delete/{id}
        /// Elimina una lista de reproducción.
        /// </summary>
        /// <param name="id">Identificador de la lista a eliminar.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _db.Listas.FindAsync(id);
            if (entity != null)
            {
                _db.Listas.Remove(entity);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

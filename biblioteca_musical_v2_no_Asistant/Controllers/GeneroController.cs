using System;
using biblioteca_musical_v2_no_Asistant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace biblioteca_musical_v2_no_Asistant.Controllers
{
    public class GeneroController : Controller
    {
        private readonly BibliotecaMusicalContext _db;
        public GeneroController(BibliotecaMusicalContext db) => _db = db;

        // GET: /Genero/Index
        public async Task<IActionResult> Index()
        {
            var list = await _db.Generos.ToListAsync();
            return View(list);
        }

        /// <summary>
        /// GET: /Genero/Create
        /// Muestra el formulario para crear un nuevo G�nero.
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: /Genero/Create
        /// Crea un nuevo g�nero.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Genero model)
        {
            if (!ModelState.IsValid)
                // Si la validaci�n falla, volvemos a mostrar el formulario con errores
                return View(model);

            model.FechaCreacion = DateTime.UtcNow;
            model.FechaModificacion = DateTime.UtcNow;
            _db.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// GET: /Genero/Edit/{id}
        /// Muestra el formulario de edici�n para el g�nero especificado.
        /// </summary>
        /// <param name="id">ID del g�nero a editar.</param>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _db.Generos.FindAsync(id);
            if (entity == null)
                return NotFound();

            return View(entity);
        }

        /// <summary>
        /// POST: /Genero/Edit/{id}
        /// Actualiza un g�nero existente.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Genero model)
        {
            if (id != model.IdGenero || !ModelState.IsValid)
            {
                // Si hay desajuste de ID o validaci�n falla, volvemos a mostrar el formulario
                return View(model);
            }

            var entity = await _db.Generos.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.Nombre = model.Nombre;
            entity.Descripcion = model.Descripcion;
            entity.FechaModificacion = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// POST: /Genero/Delete/{id}
        /// Elimina un g�nero de la base de datos.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _db.Generos.FindAsync(id);
            if (entity != null)
            {
                _db.Generos.Remove(entity);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

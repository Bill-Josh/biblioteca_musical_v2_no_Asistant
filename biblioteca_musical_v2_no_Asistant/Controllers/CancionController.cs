        using biblioteca_musical_v2_no_Asistant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace biblioteca_musical_v2_no_Asistant.Controllers
{
    public class CancionController : Controller
    {
        private readonly BibliotecaMusicalContext _db;
        public CancionController(BibliotecaMusicalContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var list = await _db.Canciones
                                .Include(c => c.IdArtistaNavigation)
                                .Include(c => c.IdGeneroNavigation)
                                .ToListAsync();

            ViewBag.Artistas = new SelectList(
                await _db.Artistas.ToListAsync(), "IdArtista", "Nombre");
            ViewBag.Generos = new SelectList(
                await _db.Generos.ToListAsync(), "IdGenero", "Nombre");

            return View(list);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cancion m)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index));
            m.FechaCreacion = DateTime.UtcNow;
            m.FechaModificacion = DateTime.UtcNow;
            _db.Add(m);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cancion m)
        {
            if (id != m.IdCancion || !ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            var e = await _db.Canciones.FindAsync(id);
            if (e == null) return NotFound();

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

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _db.Canciones.FindAsync(id);
            if (e != null)
            {
                _db.Canciones.Remove(e);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcCRUD.Models;

namespace MvcCRUD.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        private readonly MvcCrudContext _context;

        public AlumnoMateriaController(MvcCrudContext context)
        {
            _context = context;
        }

        // GET: AlumnoMateria
        public async Task<IActionResult> Index()
        {
            var mvcCrudContext = _context.AlumnoMateria.Include(a => a.IdAlumnoNavigation).Include(a => a.IdMateriaNavigation);
            return View(await mvcCrudContext.ToListAsync());
        }

        // GET: AlumnoMateria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumnoMaterium = await _context.AlumnoMateria
                .Include(a => a.IdAlumnoNavigation)
                .Include(a => a.IdMateriaNavigation)
                .FirstOrDefaultAsync(m => m.IdAlumnoMateria == id);
            if (alumnoMaterium == null)
            {
                return NotFound();
            }

            return View(alumnoMaterium);
        }

        // GET: AlumnoMateria/Create
        public IActionResult Create()
        {
            ViewData["IdAlumno"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante");
            ViewData["IdMateria"] = new SelectList(_context.Materia, "IdMateria", "IdMateria");
            ViewData["CarnetAlumno"] = new SelectList(_context.Estudiantes, "Carnet", "Carnet");
            ViewData["Nombre_Materia"] = new SelectList(_context.Materia, "Nombre", "Nombre");
            ViewData["NombreAlumno"] = new SelectList(_context.Estudiantes, "Nombre", "Nombre");

            return View();
        }

        // POST: AlumnoMateria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAlumnoNavigation,IdMateriaNavigation")] AlumnoMaterium alumnoMaterium)
        {
            if (ModelState.IsValid)
            {
                var estudiante = _context.Estudiantes.FirstOrDefault(e => e.Carnet == alumnoMaterium.IdAlumnoNavigation.Carnet);
                var materia = _context.Materia.FirstOrDefault(e => e.Nombre == alumnoMaterium.IdMateriaNavigation.Nombre);

                alumnoMaterium.IdAlumnoNavigation = estudiante;
                alumnoMaterium.IdMateriaNavigation = materia;

                _context.Add(alumnoMaterium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlumno"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante", alumnoMaterium.IdAlumno);
            ViewData["IdMateria"] = new SelectList(_context.Materia, "IdMateria", "IdMateria", alumnoMaterium.IdMateria);
            return View(alumnoMaterium);
        }

        // GET: AlumnoMateria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumnoMaterium = await _context.AlumnoMateria.FindAsync(id);
            if (alumnoMaterium == null)
            {
                return NotFound();
            }
            ViewData["IdAlumno"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante", alumnoMaterium.IdAlumno);
            ViewData["IdMateria"] = new SelectList(_context.Materia, "IdMateria", "IdMateria", alumnoMaterium.IdMateria);
            return View(alumnoMaterium);
        }

        // POST: AlumnoMateria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAlumnoMateria,IdAlumno,IdMateria")] AlumnoMaterium alumnoMaterium)
        {
            if (id != alumnoMaterium.IdAlumnoMateria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumnoMaterium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoMateriumExists(alumnoMaterium.IdAlumnoMateria))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlumno"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante", alumnoMaterium.IdAlumno);
            ViewData["IdMateria"] = new SelectList(_context.Materia, "IdMateria", "IdMateria", alumnoMaterium.IdMateria);
            return View(alumnoMaterium);
        }

        // GET: AlumnoMateria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumnoMaterium = await _context.AlumnoMateria
                .Include(a => a.IdAlumnoNavigation)
                .Include(a => a.IdMateriaNavigation)
                .FirstOrDefaultAsync(m => m.IdAlumnoMateria == id);
            if (alumnoMaterium == null)
            {
                return NotFound();
            }

            return View(alumnoMaterium);
        }

        // POST: AlumnoMateria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alumnoMaterium = await _context.AlumnoMateria.FindAsync(id);
            _context.AlumnoMateria.Remove(alumnoMaterium);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoMateriumExists(int id)
        {
            return _context.AlumnoMateria.Any(e => e.IdAlumnoMateria == id);
        }
    }
}

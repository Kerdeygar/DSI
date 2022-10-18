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
    public class NotasController : Controller
    {
        private readonly MvcCrudContext _context;

        public NotasController(MvcCrudContext context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            var mvcCrudContext = _context.Notas.Include(n => n.IdMateriaAlumnoNavigation).Include(am => am.IdMateriaAlumnoNavigation.IdAlumnoNavigation);

            return View(await mvcCrudContext.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.IdMateriaAlumnoNavigation)
                .FirstOrDefaultAsync(m => m.IdNota == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // GET: Notas/Create
        public IActionResult Create()
        {
            ViewData["IdMateriaAlumno"] = new SelectList(_context.AlumnoMateria, "IdAlumnoMateria", "IdAlumnoMateria");
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNota,IdMateriaAlumno,ParcialI,ParcialIi,Sistematicos,NotaFinal,ConvocatoriaI,NfconvocatoriaI,ConvocatoriaIi,NfconvocatoriaIi")] Nota nota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMateriaAlumno"] = new SelectList(_context.AlumnoMateria, "IdAlumnoMateria", "IdAlumnoMateria", nota.IdMateriaAlumno);
            return View(nota);
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }
            ViewData["IdMateriaAlumno"] = new SelectList(_context.AlumnoMateria, "IdAlumnoMateria", "IdAlumnoMateria", nota.IdMateriaAlumno);
            return View(nota);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNota,IdMateriaAlumno,ParcialI,ParcialIi,Sistematicos,NotaFinal,ConvocatoriaI,NfconvocatoriaI,ConvocatoriaIi,NfconvocatoriaIi")] Nota nota)
        {
            if (id != nota.IdNota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaExists(nota.IdNota))
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
            ViewData["IdMateriaAlumno"] = new SelectList(_context.AlumnoMateria, "IdAlumnoMateria", "IdAlumnoMateria", nota.IdMateriaAlumno);
            return View(nota);
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.IdMateriaAlumnoNavigation)
                .FirstOrDefaultAsync(m => m.IdNota == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nota = await _context.Notas.FindAsync(id);
            _context.Notas.Remove(nota);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaExists(int id)
        {
            return _context.Notas.Any(e => e.IdNota == id);
        }
    }
}

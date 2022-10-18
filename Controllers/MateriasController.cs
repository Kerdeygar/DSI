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
    public class MateriasController : Controller
    {
        private readonly MvcCrudContext _context;

        public MateriasController(MvcCrudContext context)
        {
            _context = context;
        }

        // GET: Materias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Materia.ToListAsync());
        }

        // GET: Materias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materium = await _context.Materia
                .FirstOrDefaultAsync(m => m.IdMateria == id);
            if (materium == null)
            {
                return NotFound();
            }

            return View(materium);
        }

        // GET: Materias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Materias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMateria,Nombre,Descripcion")] Materium materium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(materium);
        }

        // GET: Materias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materium = await _context.Materia.FindAsync(id);
            if (materium == null)
            {
                return NotFound();
            }
            return View(materium);
        }

        // POST: Materias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMateria,Nombre,Descripcion")] Materium materium)
        {
            if (id != materium.IdMateria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriumExists(materium.IdMateria))
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
            return View(materium);
        }

        // GET: Materias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materium = await _context.Materia
                .FirstOrDefaultAsync(m => m.IdMateria == id);
            if (materium == null)
            {
                return NotFound();
            }

            return View(materium);
        }

        // POST: Materias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materium = await _context.Materia.FindAsync(id);
            _context.Materia.Remove(materium);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriumExists(int id)
        {
            return _context.Materia.Any(e => e.IdMateria == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trello_back.Models;

namespace Trello_back.Controllers
{
    public class ListeController : Controller
    {
        private readonly TrelloContext _context;

        public ListeController(TrelloContext context)
        {
            _context = context;
        }

        // GET: Liste
        public async Task<IActionResult> Index()
        {
            var trelloContext = _context.Listes.Include(l => l.IdProjetNavigation);
            return View(await trelloContext.ToListAsync());
        }

        // GET: Liste/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liste = await _context.Listes
                .Include(l => l.IdProjetNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (liste == null)
            {
                return NotFound();
            }

            return View(liste);
        }

        // GET: Liste/Create
        public IActionResult Create()
        {
            ViewData["IdProjet"] = new SelectList(_context.Projets, "Id", "Id");
            return View();
        }

        // POST: Liste/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,IdProjet")] Liste liste)
        {
            if (ModelState.IsValid)
            {
                _context.Add(liste);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProjet"] = new SelectList(_context.Projets, "Id", "Id", liste.IdProjet);
            return View(liste);
        }

        // GET: Liste/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liste = await _context.Listes.FindAsync(id);
            if (liste == null)
            {
                return NotFound();
            }
            ViewData["IdProjet"] = new SelectList(_context.Projets, "Id", "Id", liste.IdProjet);
            return View(liste);
        }

        // POST: Liste/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,IdProjet")] Liste liste)
        {
            if (id != liste.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(liste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListeExists(liste.Id))
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
            ViewData["IdProjet"] = new SelectList(_context.Projets, "Id", "Id", liste.IdProjet);
            return View(liste);
        }

        // GET: Liste/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liste = await _context.Listes
                .Include(l => l.IdProjetNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (liste == null)
            {
                return NotFound();
            }

            return View(liste);
        }

        // POST: Liste/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var liste = await _context.Listes.FindAsync(id);
            if (liste != null)
            {
                _context.Listes.Remove(liste);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListeExists(int id)
        {
            return _context.Listes.Any(e => e.Id == id);
        }
    }
}

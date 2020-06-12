using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EF_MVC.Data;
using EF_MVC.Models.Sttafy;

namespace EF_MVC.Controllers
{
    public class EntitesController : Controller
    {
        private readonly SchoolContext _context;

        public EntitesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Entites
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Entites.Include(e => e.Directeur);
            //    var nbDepartement = _context.Poles.Select(d => d.Name).ToList().Distinct().Count();
            //  ViewData["nbDepartement"] = nbDepartement;

            return View(await schoolContext.ToListAsync());
        }

        // GET: Entites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entite = await _context.Entites
                .Include(e => e.Directeur)
                .FirstOrDefaultAsync(m => m.ID == id);

            // var poles = await _context.Poles.Where(d => d.EntiteID == id).Select(d => d.Name).ToListAsync();
            var poles = await _context.Entites.Include(s => s.Poles).FirstOrDefaultAsync(m => m.ID == id); ;

            if (entite == null)
            {
                return NotFound();
            }
            //ViewBag.LesPoles = poles;
            return View(poles);
        }

        // GET: Entites/Create
        public IActionResult Create()
        {
            ViewData["CollaborateurID"] = new SelectList(_context.Collaborateurs, "ID", "FullName");
            return View();
        }

        // POST: Entites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,CollaborateurID")] Entite entite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CollaborateurID"] = new SelectList(_context.Collaborateurs, "ID", "FullName", entite.CollaborateurID);
            return View(entite);
        }

        // GET: Entites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entite = await _context.Entites.FindAsync(id);
            if (entite == null)
            {
                return NotFound();
            }
            ViewData["CollaborateurID"] = new SelectList(_context.Collaborateurs, "ID", "FullName", entite.CollaborateurID);
            return View(entite);
        }

        // POST: Entites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,CollaborateurID")] Entite entite)
        {
            if (id != entite.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntiteExists(entite.ID))
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
            ViewData["CollaborateurID"] = new SelectList(_context.Collaborateurs, "ID", "EmailAddress", entite.CollaborateurID);
            return View(entite);
        }

        // GET: Entites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entite = await _context.Entites
                .Include(e => e.Directeur)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (entite == null)
            {
                return NotFound();
            }

            return View(entite);
        }

        // POST: Entites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entite = await _context.Entites.FindAsync(id);
            _context.Entites.Remove(entite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntiteExists(int id)
        {
            return _context.Entites.Any(e => e.ID == id);
        }
    }
}

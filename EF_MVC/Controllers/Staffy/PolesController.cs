using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EF_MVC.Data;
using EF_MVC.Models.Sttafy;
using Microsoft.AspNetCore.Components.Forms;

namespace EF_MVC.Controllers
{
    public class PolesController : Controller
    {
        private readonly SchoolContext _context;

        public PolesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Poles
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Poles.Include(p => p.Manager);
            //ViewData["CollaborateurID"] = new SelectList(_context.Collaborateurs, "ID", "FullName");
            return View(await schoolContext.ToListAsync());
        }

        // GET: Poles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pole = await _context.Poles
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pole == null)
            {
                return NotFound();
            }

            var entitePole = await _context.Entites
                .FirstOrDefaultAsync(m => m.ID == pole.EntiteID);

            ViewData["EntiteName"] = entitePole.Name;

            return View(pole);
        }

        // GET: Poles/Create
        public IActionResult Create()
        {
            ViewData["CollaborateurID"] = new SelectList(_context.Collaborateurs, "ID", "FullName");
            ViewData["EntiteID"] = new SelectList(_context.Entites, "ID", "Name");

            return View();
        }

        // POST: Poles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,EntiteID,CollaborateurID")] Pole pole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CollaborateurID"] = new SelectList(_context.Collaborateurs, "ID", "EmailAddress", pole.CollaborateurID);
            return View(pole);
        }

        // GET: Poles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pole = await _context.Poles.FindAsync(id);
            if (pole == null)
            {
                return NotFound();
            }
            ViewData["CollaborateurID"] = new SelectList(_context.Collaborateurs, "ID", "FullName", pole.CollaborateurID);
            ViewData["EntiteID"] = new SelectList(_context.Entites, "ID", "Name",pole.EntiteID);
            return View(pole);
        }

        // POST: Poles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,EntiteID,CollaborateurID")] Pole pole)
        {
            if (id != pole.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoleExists(pole.ID))
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
            ViewData["CollaborateurID"] = new SelectList(_context.Collaborateurs, "ID", "EmailAddress", pole.CollaborateurID);
            return View(pole);
        }

        // GET: Poles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pole = await _context.Poles
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pole == null)
            {
                return NotFound();
            }

            return View(pole);
        }

        // POST: Poles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pole = await _context.Poles.FindAsync(id);
            _context.Poles.Remove(pole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoleExists(int id)
        {
            return _context.Poles.Any(e => e.ID == id);
        }
    }
}

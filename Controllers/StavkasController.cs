using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabLastGer8;

namespace LabLastGer8.Controllers
{
    public class StavkasController : Controller
    {
        private readonly LecturerContext _context;

        public StavkasController(LecturerContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index(string sortOrder)
        {

            ViewData["CountSortParam"] = String.IsNullOrEmpty(sortOrder) ? "count_desc" : "";

            var countS = from s in _context.Stavkas
                        select s;
            switch (sortOrder)
            {
                case "count_desc":
                   countS = countS.OrderByDescending(s => s.Count);
                    break;
                default:
                   countS = countS.OrderBy(s => s.Count);
                   break;
            }

            return View(await countS.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stavka = await _context.Stavkas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stavka == null)
            {
                return NotFound();
            }

            return View(stavka);
        }

       
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Count")] Stavka stavka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stavka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stavka);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stavka = await _context.Stavkas.FindAsync(id);
            if (stavka == null)
            {
                return NotFound();
            }
            return View(stavka);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Count")] Stavka stavka)
        {
            if (id != stavka.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stavka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StavkaExists(stavka.Id))
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
            return View(stavka);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stavka = await _context.Stavkas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stavka == null)
            {
                return NotFound();
            }

            return View(stavka);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stavka = await _context.Stavkas.FindAsync(id);
            _context.Stavkas.Remove(stavka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StavkaExists(int id)
        {
            return _context.Stavkas.Any(e => e.Id == id);
        }
    }
}

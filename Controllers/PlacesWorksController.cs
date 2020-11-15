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
    public class PlacesWorksController : Controller
    {
        private readonly LecturerContext _context;

        public PlacesWorksController(LecturerContext context)
        {
            _context = context;
        }

        // GET: PlacesWorks
        public async Task<IActionResult> Index(string sortOrder)
        {

            ViewData["PlaceNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "placeName_desc" : "";

            var placesWork = from s in _context.PlacesWorks
                            select s;
            switch (sortOrder)
            {
                case "placeName_desc":
                    placesWork = placesWork.OrderByDescending(s => s.PlaceName);
                    break;
                default:
                    placesWork = placesWork.OrderBy(s => s.PlaceName);
                    break;
            }
            
            return View(await placesWork.ToListAsync());
        }

        // GET: PlacesWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placesWork = await _context.PlacesWorks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placesWork == null)
            {
                return NotFound();
            }

            return View(placesWork);
        }

        // GET: PlacesWorks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlacesWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlaceName")] PlacesWork placesWork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(placesWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(placesWork);
        }

        // GET: PlacesWorks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placesWork = await _context.PlacesWorks.FindAsync(id);
            if (placesWork == null)
            {
                return NotFound();
            }
            return View(placesWork);
        }

        // POST: PlacesWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlaceName")] PlacesWork placesWork)
        {
            if (id != placesWork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(placesWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlacesWorkExists(placesWork.Id))
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
            return View(placesWork);
        }

        // GET: PlacesWorks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placesWork = await _context.PlacesWorks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placesWork == null)
            {
                return NotFound();
            }

            return View(placesWork);
        }

        // POST: PlacesWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var placesWork = await _context.PlacesWorks.FindAsync(id);
            _context.PlacesWorks.Remove(placesWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlacesWorkExists(int id)
        {
            return _context.PlacesWorks.Any(e => e.Id == id);
        }
    }
}

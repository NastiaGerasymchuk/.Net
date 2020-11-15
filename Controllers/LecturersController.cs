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
    public class LecturersController : Controller
    {
        private readonly LecturerContext _context;

        public LecturersController(LecturerContext context)
        {
            _context = context;
        }

        // GET: Lecturers
        public async Task<IActionResult> Index(string sortOrder, string currentFilter,
    string searchString,
    int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["PostNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "postName_desc" : "";
            ViewData["StavkaCountSortParam"] = sortOrder == "StavkaCount" ? "stavka_count_desc" : "StavkaCount";
           
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var lecturers = from s in _context.Lecturers.Include(p => p.Post).Include(p=>p.Person).Include(s=>s.Stavka).Include(p=>p.PlaceWork)
                                  select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                lecturers = lecturers.Where(s => s.Person.LastName.Contains(searchString)
                                       || s.Person.FirstName.Contains(searchString)
                                       || s.Person.MiddleName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "postName_desc":
                    lecturers = lecturers.OrderByDescending(s => s.Post.PostName);
                    break;
                case "stavka_count_desc":
                    lecturers = lecturers.OrderByDescending(s => s.Stavka.Count);
                    break;
                case "StavkaCount":
                    lecturers = lecturers.OrderByDescending(s => s.Stavka.Count);
                    break;
                default:
                    lecturers = lecturers.OrderBy(s => s.Post.PostName);
                    break;
            }

            //return View(await lecturers.ToListAsync());
            int pageSize = 3;
            return View(await PaginatedList<Lecturer>.CreateAsync(lecturers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Lecturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturers
                .Include(l => l.Person)
                .Include(l => l.PlaceWork)
                .Include(l => l.Post)
                .Include(l => l.Stavka)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
        }

        // GET: Lecturers/Create
        public IActionResult Create()
        {
            var persons = (from p in _context.Persons
                           select new
                           {
                               Id = p.Id,
                               Pers = new Person() { Id = p.Id, FirstName = p.FirstName, LastName = p.LastName, MiddleName = p.MiddleName, PhoneNumber = p.PhoneNumber, AddressId = p.AddressId }
                           }).ToList();

           
            ViewData["PersonId"] = new SelectList(persons, "Id", "Pers");
            ViewData["PlaceWorkId"] = new SelectList(_context.PlacesWorks, "Id", "PlaceName");
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "PostName");
            ViewData["StavkaId"] = new SelectList(_context.Stavkas, "Id", "Count");
            return View();
        }

        // POST: Lecturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonId,PlaceWorkId,StavkaId,PostId,Characteristic")] Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", lecturer.PersonId);
            ViewData["PlaceWorkId"] = new SelectList(_context.PlacesWorks, "Id", "Id", lecturer.PlaceWork.PlaceName);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", lecturer.Post.PostName);
            ViewData["StavkaId"] = new SelectList(_context.Stavkas, "Id", "Id", lecturer.Stavka);
            return View(lecturer);
        }

        // GET: Lecturers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null)
            {
                return NotFound();
            }
            var persons = (from p in _context.Persons
                           select new
                           {
                               Id = p.Id,
                               Pers = new Person() { Id = p.Id, FirstName = p.FirstName, LastName = p.LastName, MiddleName = p.MiddleName, PhoneNumber = p.PhoneNumber, AddressId = p.AddressId }
                           }).ToList();


            ViewData["PersonId"] = new SelectList(persons, "Id", "Pers");           
            ViewData["PlaceWorkId"] = new SelectList(_context.PlacesWorks, "Id", "PlaceName");
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "PostName");
            ViewData["StavkaId"] = new SelectList(_context.Stavkas, "Id", "Count");
            return View(lecturer);
        }

        // POST: Lecturers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonId,PlaceWorkId,StavkaId,PostId,Characteristic")] Lecturer lecturer)
        {
            if (id != lecturer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecturer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LecturerExists(lecturer.Id))
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
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", lecturer.PersonId);
            ViewData["PlaceWorkId"] = new SelectList(_context.PlacesWorks, "Id", "Id", lecturer.PlaceWorkId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", lecturer.PostId);
            ViewData["StavkaId"] = new SelectList(_context.Stavkas, "Id", "Id", lecturer.StavkaId);
            return View(lecturer);
        }

        // GET: Lecturers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturers
                .Include(l => l.Person)
                .Include(l => l.PlaceWork)
                .Include(l => l.Post)
                .Include(l => l.Stavka)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
        }

        // POST: Lecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            _context.Lecturers.Remove(lecturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LecturerExists(int id)
        {
            return _context.Lecturers.Any(e => e.Id == id);
        }
    }
}

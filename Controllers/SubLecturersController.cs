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
    public class SubLecturersController : Controller
    {
        private readonly LecturerContext _context;

        public SubLecturersController(LecturerContext context)
        {
            _context = context;
        }

        // GET: SubLecturers
        public async Task<IActionResult> Index(string sortOrder)
        {
            var lecturerContext = _context.SubLecturers.Include(s => s.Lecturer).ThenInclude(p => p.Person).Include(s => s.Subject);
            ViewData["SubjectNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "subjectName_desc" : "";
            ViewData["LastNameSortParam"] =sortOrder == "LastName" ? "l_name_desc" : "LastName";

            var subjects = from s in lecturerContext
                               select s;
                switch (sortOrder)
                {
                    case "subjectName_desc":
                        subjects = subjects.OrderByDescending(s => s.Subject.SubjName);
                        break;
                    case "l_name_desc":
                        subjects = lecturerContext.OrderByDescending(s => s.Lecturer.Person.LastName);
                        break;
                    case "LastName":
                       subjects = lecturerContext.OrderBy(s => s.Lecturer.Person.LastName);
                        break;
                    default:
                        subjects = subjects.OrderBy(s => s.Subject.SubjName);
                        break;
                }

               
           
            return View(await subjects.ToListAsync());
        }

        // GET: SubLecturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subLecturer = await _context.SubLecturers
                .Include(s => s.Lecturer)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subLecturer == null)
            {
                return NotFound();
            }

            return View(subLecturer);
        }

        // GET: SubLecturers/Create
        public IActionResult Create()
        {

            //Subjects = new SelectList(_context.Subjects, "Id", "SubjName");
            var lect = (from l in _context.Lecturers
                            //join ls in _context.SubLecturers
                            //on l.Id equals ls.LecturerId

                        select new
                        {
                            lecturerId = l.Id,
                            personId = l.PersonId

                        }).Join(_context.Persons.Include(a => a.Address), // второй набор
          p => p.personId, // свойство-селектор объекта из первого набора
          c => c.Id, // свойство-селектор объекта из второго набора
          (p, c) => new // результат
          {
              Id = p.lecturerId,
              Pers = new Person() { FirstName = c.FirstName, LastName = c.LastName, MiddleName = c.MiddleName, PhoneNumber = c.PhoneNumber, AddressId = c.AddressId, Address = c.Address }
          }).ToList();
            //Lecturers = new SelectList(lect, "Id", "Pers");

            ViewData["LecturerId"] = new SelectList(lect, "Id", "Pers");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "SubjName");
            return View();
            
        }

        // POST: SubLecturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LecturerId,SubjectId,Hours")] SubLecturer subLecturer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subLecturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Id", subLecturer.LecturerId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", subLecturer.SubjectId);
            return View(subLecturer);
        }

        // GET: SubLecturers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subLecturer = await _context.SubLecturers.FindAsync(id);
            if (subLecturer == null)
            {
                return NotFound();
            }
            //ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Id", subLecturer.LecturerId);
            //ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", subLecturer.SubjectId);

            //Subjects = new SelectList(_context.Subjects, "Id", "SubjName");
            var lect = (from l in _context.Lecturers
                            //join ls in _context.SubLecturers
                            //on l.Id equals ls.LecturerId

                        select new
                        {
                            lecturerId = l.Id,
                            personId = l.PersonId

                        }).Join(_context.Persons.Include(a => a.Address), // второй набор
          p => p.personId, // свойство-селектор объекта из первого набора
          c => c.Id, // свойство-селектор объекта из второго набора
          (p, c) => new // результат
          {
              Id = p.lecturerId,
              Pers = new Person() { FirstName = c.FirstName, LastName = c.LastName, MiddleName = c.MiddleName, PhoneNumber = c.PhoneNumber, AddressId = c.AddressId, Address = c.Address }
          }).ToList();
            //Lecturers = new SelectList(lect, "Id", "Pers");

            ViewData["LecturerId"] = new SelectList(lect, "Id", "Pers");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "SubjName");
            
            //return View();
            return View(subLecturer);
        }

        // POST: SubLecturers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LecturerId,SubjectId,Hours")] SubLecturer subLecturer)
        {
            if (id != subLecturer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subLecturer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubLecturerExists(subLecturer.Id))
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
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Id", subLecturer.LecturerId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", subLecturer.SubjectId);
            return View(subLecturer);
        }

        // GET: SubLecturers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subLecturer = await _context.SubLecturers
                .Include(s => s.Lecturer)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subLecturer == null)
            {
                return NotFound();
            }

            return View(subLecturer);
        }

        // POST: SubLecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subLecturer = await _context.SubLecturers.FindAsync(id);
            _context.SubLecturers.Remove(subLecturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubLecturerExists(int id)
        {
            return _context.SubLecturers.Any(e => e.Id == id);
        }
    }
}

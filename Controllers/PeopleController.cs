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
    public class PeopleController : Controller
    {
        private readonly LecturerContext _context;

        public PeopleController(LecturerContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["LastNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "lastName_desc" : "";
            ViewData["FirstNameSortParam"] = sortOrder == "FirstName" ? "f_name_desc" : "FirstName";


            var lecturerContext = from s in _context.Persons.Include(p => p.Address)
            select s;
            switch (sortOrder)
            {
                case "lastName_desc":
                    lecturerContext =lecturerContext.OrderByDescending(s => s.LastName);
                    break;
                case "f_name_desc":
                    lecturerContext = lecturerContext.OrderByDescending(s => s.FirstName);
                    break;
                case "FirstName":
                    lecturerContext = lecturerContext.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    lecturerContext = lecturerContext.OrderBy(s => s.LastName);
                    break;
            }
            //return View(await students.AsNoTracking().ToListAsync());
            return View(await lecturerContext.ToListAsync());
            
            //return View(await lecturerContext.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(p => p.Address)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            //ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "City");
            //return View();
            var addresses = (from a in _context.Addresses
                             select new
                             {
                                 Id = a.Id,
                                 Add = new Address() { Id = a.Id, Country = a.Country, City = a.City, HouseNumber = a.HouseNumber, Street = a.Street }
                             }).ToList();

            ViewBag.Addresses = new SelectList(addresses, "Id","Add");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,MiddleName,PhoneNumber,AddressId")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", person.AddressId);
            return View(person);
           
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            var addresses = (from a in _context.Addresses
                             select new
                             {
                                 Id = a.Id,
                                 Add = new Address() { Id = a.Id, Country = a.Country, City = a.City, HouseNumber = a.HouseNumber, Street = a.Street }
                             }).ToList();

           ViewBag.Addresses = new SelectList(addresses, "Id", "Add");
            //return View();
            //ViewData["AddressId"] = new SelectList(addresses, "Id", "Add", person.AddressId);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,MiddleName,PhoneNumber,AddressId")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            var addresses = (from a in _context.Addresses
                             select new
                             {
                                 Id = a.Id,
                                 Add = new Address() { Id = a.Id, Country = a.Country, City = a.City, HouseNumber = a.HouseNumber, Street = a.Street }
                             }).ToList();
            ViewBag.Addresses = new SelectList(addresses, "Id", "Add");
            // ViewData["AddressId"] = new SelectList(addresses, "Id", "Add", person.AddressId);
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(p => p.Address)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}

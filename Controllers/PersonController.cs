using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_ef.Models;
using System.Text.Encodings.Web;

namespace mvc_ef.Controllers
{
    public class PersonController : Controller
    {
        private readonly SqliteContext _context;

        public PersonController(SqliteContext context)
        {
            _context = context;
        }

        // GET: Person
        public IActionResult Index()
        {
            var sqliteContext = _context.People.Include(p => p.City).ThenInclude(c => c.Country);
            return View(sqliteContext);
        }

        // GET: Person/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person =  _context.People
                .Include(p => p.City)
		.ThenInclude(c => c.Country)
                .FirstOrDefault(m => m.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            ViewData["Cities"] = new SelectList(_context.Cities, "Id", "CityName");
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public IActionResult Create([Bind("PersonName,CityId")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
		_context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cities"] = new SelectList(_context.Cities, "Id", "CityName", person.City.CityName);
	    return View(person);
        }

        // GET: Person/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
	 
            var person =  _context.People.Include(c => c.City).FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

	    ViewData["Cities"] = new SelectList(_context.Cities, "Id", "CityName", person.City.CityName);
            return View(person);
        }

        // POST: Person/Edit/5
        [HttpPost]
        public  IActionResult Edit(int id, [Bind("Id,PersonName,CityId")] Person person)
        {
	    if (!PersonExists(person.Id))
	    {
		return NotFound();
	    }
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
		_context.Update(person);
		_context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cities"] = new SelectList(_context.Cities, "Id", "CityName", person.City.CityName);
            return View(person);
        }

        // GET: Person/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person =  _context.People
                .Include(p => p.City)
                .FirstOrDefault(m => m.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var person =  _context.People.Find(id);
            _context.People.Remove(person);
	    _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}

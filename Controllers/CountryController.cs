using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_ef.Models;

namespace mvc_ef.Controllers
{
    public class CountryController : Controller
    {
        private readonly SqliteContext _context;

        public CountryController(SqliteContext context)
        {
            _context = context;
        }

        // GET: Country
        public IActionResult Index()
        {
            return View( _context.Countries);
        }

        // GET: Country/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country =  _context.Countries
                .FirstOrDefault(m => m.Id == id);

            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Country/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Country/Create
        [HttpPost]
        public IActionResult Create([Bind("CountryName")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
		_context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Country/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country =  _context.Countries.Find(id);

	    if (country == null)
            {
                return NotFound();
            }

	    return View(country);
        }

        // POST: Country/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,CountryName")] Country country)
        {
	    if (!CountryExists(country.Id))
	    {
		return NotFound();
	    }

            if (id != country.Id)
            {
                return NotFound();
            }
	    
            if (ModelState.IsValid)
            {
		_context.Update(country);
		_context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Country/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country =  _context.Countries
                .FirstOrDefault(m => m.Id == id);

            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Country/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var country =  _context.Countries.Find(id);
            _context.Countries.Remove(country);
	    _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}

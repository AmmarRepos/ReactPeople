using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_ef.Models;

namespace mvc_ef.Controllers
{
    public class CityController : Controller
    {
        private readonly SqliteContext _context;

        public CityController(SqliteContext context)
        {
            _context = context;
        }

        // GET: City
        public IActionResult Index()
        {
            var sqliteContext = _context.Cities.Include(c => c.Country);
            return View(sqliteContext);
        }

        // GET: City/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city =  _context.Cities
                .Include(c => c.Country)
                .FirstOrDefault(cc => cc.Id == id);

            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: City/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName");
            return View();
        }

        // POST: City/Create
        [HttpPost]
        public IActionResult Create([Bind("CityName,CountryId")] City city)
        {
            if (ModelState.IsValid)
            {
                _context.Add(city);
		_context.SaveChanges();
		return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryId", city.Country.CountryName);
            return View(city);
        }

        // GET: City/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city =  _context.Cities
		.Include(c => c.Country)
		.FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            ViewData["Countries"] = new SelectList(_context.Countries, "Id", "CountryName", city.Country.CountryName);
            return View(city);
        }

        // POST: City/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,CityName,CountryId")] City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

	    if (!CityExists(city.Id))
	    {
		return NotFound();
	    }

            if (ModelState.IsValid)
            {
		_context.Update(city);
		_context.SaveChanges();
		return RedirectToAction(nameof(Index));
            }

            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryId", city.Country.CountryName);
            return View(city);
        }

        // GET: City/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city =  _context.Cities
                .Include(c => c.Country)
                .FirstOrDefault(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var city =  _context.Cities.Find(id);
            _context.Cities.Remove(city);
	    _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}

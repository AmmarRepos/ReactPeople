using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_ef.Models;

namespace mvc_ef.Controllers
{
    public class LanguageController : Controller
    {
        private readonly SqliteContext _context;

        public LanguageController(SqliteContext context)
        {
            _context = context;
        }

        // GET: Language
        public IActionResult Index()
        {
            return View( _context.Languages);
        }

        // GET: Language/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language =  _context.Languages
                .FirstOrDefault(m => m.Id == id);

            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // GET: Language/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Language/Create
	[HttpPost]
	public IActionResult Create([Bind("LanguageName")] Language language)
        {
            if (ModelState.IsValid)
            {
                _context.Add(language);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        // GET: Language/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language =  _context.Languages.Find(id);

            if (language == null)
            {
                return NotFound();
            }
            return View(language);
        }

        // POST: Language/Edit/5
        [HttpPost]
        public  IActionResult Edit(int id, [Bind("Id,LanguageName")] Language language)
        {
	    if (!LanguageExists(language.Id))
	    {
		return NotFound();
	    }

            if (id != language.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
		_context.Update(language);
		 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        // GET: Language/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language =  _context.Languages
                .FirstOrDefault(m => m.Id == id);

            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // POST: Language/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var language =  _context.Languages.Find(id);
            _context.Languages.Remove(language);
	    _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool LanguageExists(int id)
        {
            return _context.Languages.Any(e => e.Id == id);
        }
    }
}

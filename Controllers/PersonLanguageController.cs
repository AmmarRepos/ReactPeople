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
    public class PersonLanguageController : Controller
    {
        private readonly SqliteContext _context;

        public PersonLanguageController(SqliteContext context)
        {
            _context = context;
        }

	// PersonLanguage/Edit/5
	public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person =  _context.People
                .Include(p => p.Languages)
                .FirstOrDefault(m => m.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

	// PersonLanguage/Add/5
	public IActionResult Add(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

	    var person =  _context.People
                .Include(p => p.Languages)
		.FirstOrDefault(m => m.Id == id);
		
	    var personLanguages = person.Languages;
	    var languages =  _context.Languages.ToList().Except(personLanguages);

            if (personLanguages == null)
            {
                return NotFound();
            }

	    ViewData["PersonId"] = id;
	    ViewData["PersonName"] = person.PersonName;
	    return View(languages);
        }

	// GET:PersonLanguage/AddLanguage/5
	public IActionResult AddLanguage(int? id, int languageId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language =  _context.Languages.Find(languageId);
	    var person = _context.People.Find(id);
            if (language == null)
            {
                return NotFound();
            }
	    ViewData["PersonId"] = id;
	    ViewData["PersonName"] = person.PersonName;
	    return View(language);
        }

        // POST: PersonLangauge/AddLanguage/5
	[HttpPost, ActionName("AddLanguage")]
        public IActionResult AddLanguageConfirmed(int id, int languageId)
        {
	    var person = _context.People.Include(p => p.Languages).FirstOrDefault(p => p.Id == id);
	    var languageToAdd = _context.Languages.FirstOrDefault(l => l.Id == languageId);
	    person.Languages.Add(languageToAdd);
	    _context.SaveChanges();
	    return RedirectToAction("Edit", new { id = id });
        }

	// GET: PersonLanguage/DeleteLanguage/5
        public IActionResult DeleteLanguage(int? languageId, int id)
        {
            if (languageId == null)
            {
                return NotFound();
            }

	    var language = _context.Languages.Single(u => u.Id == languageId);
	    var person = _context.People.Single(p => p.Id == id);

            if (language == null)
            {
                return NotFound();
            }
	    ViewData["PersonId"] = id;
	    ViewData["PersonName"] = person.PersonName;
            return View(language);
        }

        // POST: PersonLangauge/DeleteLanugae/5
        [HttpPost, ActionName("DeleteLanguage")]
        public IActionResult DeleteLanguage(int id, int languageId)
        {
	    var person = _context.People.Include(p => p.Languages).FirstOrDefault(p => p.Id == id);
	    var languageToRemove = _context.Languages.FirstOrDefault(l => l.Id == languageId);
	    person.Languages.Remove(languageToRemove);
            _context.SaveChanges();
            return RedirectToAction("Edit", new { id = id });
        }

	private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}

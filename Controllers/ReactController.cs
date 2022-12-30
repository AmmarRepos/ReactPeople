//using mvc_ef.Data;
using mvc_ef.Models;
//using mvc_ef.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mvc_ef.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class ReactController : ControllerBase
  {

    private readonly SqliteContext _context;
    public ReactController(SqliteContext context)
    {
      _context = context;
    }


    [HttpGet]
    public List<Person> GetPeople()
    {
      return _context.People
        // .Include(x => x.City).Include(y => y.City.Country)
        // .Include(p=>p.Languages)
        .ToList();
    }


    [HttpGet("languages")]
    public List<Language> languages()
    {
      return _context.Languages
        // .Include(x => x.City).Include(y => y.City.Country)
        // .Include(p=>p.Languages)
        .ToList();
    }


    [HttpGet("cities/{id}")]
    public List<City> GetCities(int id)
    {
      return _context.Cities.Where(x => x.CountryId == id).ToList();
    }


    [HttpGet("countries")]
    public List<Country> GetCountries()
    {
      return _context.Countries
        // .Include(y => y.Cities)
        .ToList();
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      Person person = _context.People.Find(id);
      if (person == null) return StatusCode(404);

      _context.People.Remove(person);
      _context.SaveChanges();
      return StatusCode(200);
    }

    [HttpGet("{id}")]
    public List<Person> GetPerson(int id)
    {
	return _context.People
	    .Where(p => p.Id == id)
	    .Include(x => x.City).Include(y => y.City.Country)
	    .Include(p=>p.Languages)
	    .ToList();
    }


    
    [HttpPost("create")]
    public IActionResult Create(JsonObject personJson)
    {


	//[Bind("PersonName,CityId")]

	if (personJson !=null)
	{
	    Person person = new  Person();
	    person.PersonName = personJson["name"].ToString();
	    person.CityId = int.Parse(personJson["cityId"].ToString());
	    _context.Add(person);
	    int languageId = int.Parse(personJson["languageId"].ToString());
	    Language language = _context.Languages.FirstOrDefault(l => l.Id == languageId);
	    person.Languages.Add(language);
	    _context.SaveChanges();
	    return StatusCode(201);
	}

      // string jsonPerson = personJson.ToString();
      // PersonReact personToCreate = JsonConvert.DeserializeObject<PersonReact>(jsonPerson);

      // if (personToCreate != null)
      // {
      //   _context.People.Add(new Person { PersonName = personToCreate.Name, CityId = personToCreate.City });
      //   _context.SaveChanges();
      //   return StatusCode(200);
      // }
      Console.WriteLine(personJson["name"].ToString());
      Type type = personJson.GetType();
      Console.WriteLine(type.Name);
      return StatusCode(404);
    }
  }
}

using System.ComponentModel.DataAnnotations;

namespace mvc_ef.Models
{
    public class Person
    {
        public int Id { get; set; }
	
	[Display(Name = "Person")]	
        public string PersonName { get; set; }

	// CSharp Reference
	public City City { get; set; }
	// Foregin Key
	public int CityId { get; set; }

	// Many-to-Many (Language - Person)
	public virtual List<Language> Languages { get; set; } = new();
    }
}

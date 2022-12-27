using System.ComponentModel.DataAnnotations;

namespace mvc_ef.Models
{
    public class City
    {
        public int Id { get; set; }
	[Display(Name = "City")]
        public string CityName { get; set; } = string.Empty;

	// CSharp reference
	public Country Country { get; set; }
	// Foregin key
	public int CountryId { get; set; }


	// One-to-Many (Person - City)
	public List<Person>? People { get; set; } = new();
    }
}

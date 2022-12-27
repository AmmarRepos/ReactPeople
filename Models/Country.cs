using System.ComponentModel.DataAnnotations;

namespace mvc_ef.Models
{
    public class Country
    {
        public int Id { get; set; }

	[Display(Name = "Country")]
        public string CountryName { get; set; } = string.Empty;

	// One-to-Many (City - Country)
	public List<City> Cities { get; set; } = new();
    }
}

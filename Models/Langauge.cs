using System.ComponentModel.DataAnnotations;

namespace mvc_ef.Models
{
    public class Language
    {
        public int Id { get; set; }
	[Display(Name = "Language")]
        public string LanguageName { get; set; } = string.Empty;
	
	// Many-to-Many (Language - Person)
	public virtual List<Person> People { get; set; } = new();
    }
}

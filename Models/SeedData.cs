using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
    
namespace mvc_ef.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SqliteContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SqliteContext>>()))
            {
                // Look for any movies.
                if (context.People.Any())
                {
                    return;   // DB has been seeded
                }
		Country countryA, countryB, countryC;
		context.AddRangeAsync(new[]
		{
		    countryA = new Country() { CountryName = "CountryA"},
		    countryB = new Country() { CountryName = "CountryB"},
		    countryC = new Country() { CountryName = "CountryC"},
		});
		context.SaveChangesAsync();

		City cityA, cityB, cityC;
		context.AddRangeAsync(new[]
		{
		    cityA = new City() { CityName = "CityA", Country = countryA},
		    cityB = new City() { CityName = "CityB", Country = countryB},
		    cityC = new City() { CityName = "CityC", Country = countryC},
		});
		context.SaveChangesAsync();

		Language languageA, languageB, languageC;
		context.AddRangeAsync(new[]
		{
		    languageA = new Language() { LanguageName = "LanguageA" },
		    languageB = new Language() { LanguageName = "LanguageB" },
		    languageC = new Language() { LanguageName = "LanguageC" },
		});
		context.SaveChangesAsync();

		Person personA, personB, personC;
		context.AddRangeAsync(new[]
		{
		    personA = new Person() { PersonName = "PersonA", Languages = new() {languageA, languageB}, City = cityA },
		    personB = new Person() { PersonName = "PersonB", Languages = new() {languageB}, City = cityB },
		    personC = new Person() { PersonName = "PersonC", Languages = new() {languageC}, City = cityC},
		});
		context.SaveChangesAsync();
	    }
	}
    }
}


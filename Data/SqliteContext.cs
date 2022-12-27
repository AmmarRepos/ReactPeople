using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mvc_ef.Models;

public class SqliteContext : DbContext
{
    public SqliteContext (DbContextOptions<SqliteContext> options)
	: base(options)
    {
    }

    //DbSets
    public DbSet<mvc_ef.Models.Person> People { get; set; }
    public DbSet<mvc_ef.Models.City> Cities { get; set; }
    public DbSet<mvc_ef.Models.Country> Countries { get; set; }
    public DbSet<mvc_ef.Models.Language> Languages { get; set; }
}

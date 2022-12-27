using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using mvc_ef.Models;

var builder = WebApplication.CreateBuilder(args);

//EF related services
builder.Services.AddDbContext<SqliteContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteContext")));
// The AddDatabaseDeveloperPageExceptionFilter provides helpful error information in the development environment.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     var context = services.GetRequiredService<SqliteContext>();
//     context.Database.EnsureCreated();
//     protected override void OnModelCreating(ModelBuilder modelbuilder)
//     {
// 	base.OnModelCreating(modelbuilder);			  
// 	modelbuilder.Entity<Person>()
// 	    .HasMany(p => p.Languages)
// 	    .WithMany(c => c.People)
// 	    .UsingEntity(j => j.HasData(new { PeoplePersonId = 1, LanguagesLanguageId = 1 }));
//     }
//     // DbInitializer.Initialize(context);
// }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Person}/{action=Index}/{id?}");

app.Run();

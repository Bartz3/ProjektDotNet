using Microsoft.EntityFrameworkCore;
using Projekt.Models;
using Microsoft.Extensions.DependencyInjection;
using Projekt.Data;
using Projekt.DAL;
using Microsoft.AspNetCore.Identity;
using Projekt.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication("CookieAuthentication")
 .AddCookie("CookieAuthentication", config =>
 {
     config.Cookie.HttpOnly = true;
     config.Cookie.SecurePolicy = CookieSecurePolicy.None;
     config.Cookie.Name = "UserLoginCookie";
     config.LoginPath = "/Login/UserLogin";
     config.Cookie.SameSite = SameSiteMode.Strict;
 });

builder.Services.AddRazorPages(options => {
    options.Conventions.AuthorizeFolder("/Admin");
});

builder.Services.AddTransient<IEmployeeDB, EmployeeSqlDB>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ProjektContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjektContext") ?? throw new InvalidOperationException("Connection string 'ProjektContext' not found.")));


//builder.Services.AddDefaultIdentity<IdentityUser>()
//    .AddRoles<IdentityRole>();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});// Dodanie mechanizmu sesji
builder.Services.AddMemoryCache(); // Dodanie mechanizmu sesji


var app = builder.Build();


CreateDbIfNotExists(app);

static void CreateDbIfNotExists(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ProjektContext>();
            context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseMiddleware<ProjectMiddleware>();
app.UseProjectMiddleware();


app.UseHttpsRedirection();  

app.UseSession(); // Dodanie mechanizmu sesji
app.UseStaticFiles();        // Dodanie mechanizmu sesji

app.UseCookiePolicy();      // Rejestracja serwisu Authentication
app.UseAuthentication();    // Rejestracja serwisu Authentication

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

/*
 CREATE PROCEDURE [dbo].[sp_employeeDisplay]
AS
SELECT * FROM Employee
 
CREATE PROCEDURE [dbo].[sp_userAdd]
	@userName VARCHAR(100),
	@password VARCHAR(100),
	@Id int OUTPUT
AS
INSERT INTO Employee(userName,password) VALUES(@userName,@password)
SET @Id= @@IDENTITY
 */
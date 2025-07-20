using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectEcommerce.Data;
using ProjectEcommerce.Models;

var builder = WebApplication.CreateBuilder( args );
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString( "DefaultConnection" ) ?? throw new InvalidOperationException( "Connection string 'DefaultConnection' not found." );
builder.Services.AddDbContext<ApplicationDbContext>( options =>
    options.UseSqlServer( connectionString ) );
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>( options => options.SignIn.RequireConfirmedAccount = true )
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization( options =>
{
    options.AddPolicy( "IsAdmin", p => p.RequireRole( "Admin" ) );
    options.AddPolicy( "IsCustomer", p => p.RequireRole( "Customer" ) );
} );

var app = builder.Build();

app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);
app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}"
);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler( "/Home/Error" );
}

app.UseSession();

app.UseAuthentication();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}" );
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.SeedRolesAndAdminAsync( services );
}

app.Run();

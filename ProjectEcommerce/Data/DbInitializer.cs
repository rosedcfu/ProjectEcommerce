using Microsoft.AspNetCore.Identity;
using ProjectEcommerce.Data;
using ProjectEcommerce.Models;
using System.CodeDom.Compiler;
using System;

namespace ProjectEcommerce.Data
{
    public class DbInitializer
    {
        public static async Task SeedRolesAndAdminAsync( IServiceProvider serviceProvider )
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();


            // Crear el rol Admin
            if (!await roleManager.RoleExistsAsync( "Admin" ))
            {
                await roleManager.CreateAsync( new IdentityRole( "Admin" ) );
            }

            // Crear el usuario admin
            var adminEmail = "admin@shop.com";
            var adminUser = await userManager.FindByEmailAsync( adminEmail );
            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Admin Rose",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync( newAdmin, "Admin123!" );

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync( newAdmin, "Admin" );
                }
            }

            if (!await roleManager.RoleExistsAsync( "Customer" ))
            {
                await roleManager.CreateAsync( new IdentityRole( "Customer" ) );
            }

            if (!context.Products.Any())
            {
                var productos = new List<Product>
                {
                    new Product
                    {
                        Name = "Bolso Artesanal Marrón",
                        Description = "Bolso hecho a mano de cuero genuino.",
                        Price = 35990,
                        Stock = 10,
                        ImageUrl = "https://cdn.pixabay.com/photo/2020/03/13/04/06/handmade-4926872_640.jpg"
                    },
                    new Product
                    {
                        Name = "Bolso Beige Elegante",
                        Description = "Bolso de tela resistente y diseño elegante.",
                        Price = 28990,
                        Stock = 15,
                        ImageUrl = "https://cdn.pixabay.com/photo/2020/06/14/12/47/bag-5297725_640.jpg"
                    },
                    new Product
                    {
                        Name = "Bolso de Cuero Negro",
                        Description = "Bolso con detalles de costura hechos a mano.",
                        Price = 31990,
                        Stock = 12,
                        ImageUrl = "https://cdn.pixabay.com/photo/2018/10/06/14/48/leather-craft-3727996_640.jpg"
                    }
                };

                context.Products.AddRange( productos );
                await context.SaveChangesAsync();
            }
        }

    }
}
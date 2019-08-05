using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using E_ticaret.DataAccess.Identity;

namespace E_ticaret.DataAccess
{
    public class DbInitializer
    {
        public static async System.Threading.Tasks.Task Initialize(DataContext context, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            if(!context.Roles.Any(i=>i.Name=="Admin"))
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Admin";
                role.Description = "Yönetici Rolü";
                IdentityResult result = await roleManager.CreateAsync(role);
            }
            if (!context.Roles.Any(i => i.Name == "User"))
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "User";
                role.Description = "User Rolü";
                IdentityResult result = await roleManager.CreateAsync(role);
            }
            if (!context.Users.Any(i => i.Name == "Admin"))
            {
                var user = new ApplicationUser()
                {
                    Name="Ahmet",
                    Surname="Ateş",
                    UserName="Admin",
                    Email="ahmet.atess1@gmail.com",                    
                };


                IdentityResult x =await userManager.CreateAsync(user,"Database Admin Kullanıcısı oluşturmak için gerekli parola girilecek1.");
                IdentityResult y = await userManager.AddToRoleAsync(user,"Admin");
            }

            if (context.Products.Any())
            {
                return;
            }
            List<Category> kategoriler = new List<Category>()
            {
                new Category(){Name="Kamera",Description="Kamera Ürünleri"},
                new Category(){Name="Bilgisayar",Description="Bilgisayar Ürünleri"},
                new Category(){Name="Elektronik",Description="Elektronik Ürünleri"},
                new Category(){Name="Telefon",Description="Telefon Ürünleri"},
                new Category(){Name="Beyaz Eşya",Description="Beyaz Eşya Ürünleri"},
            };

            foreach (var kategori in kategoriler)
            {
                context.Categories.Add(kategori);
            }
            context.SaveChanges();

            List<Product> urunler = new List<Product>()
            {
                new Product(){Name="Samsung 55KU7500 55 140 Ekran [4k] Uydu Alıcılı Smart[Tizen]",Description="Samsung tv ",Price=1400, Stock=20, IsApproved=true,CategoryId=3, IsHome=true ,Image="1.jpg"},
                new Product(){Name="Canon 4000D 18-55 MM IS II (Canon Eurasia Garantili)",Description="DSLR ile öne çıkmanızı sağlayan basit hikaye anlatımı DSLR kalitesindeki fotoğraflar ve Full HD filmler ile öne çıkan hikayeler yaratmak 18 Megapiksel EOS 4000D ile sandığınızdan daha kolay. Wi-Fi * ve Canon Camera Connect uygulaması ile uyumlu akıllı telefonunuzla anında uzaktan çekim yapın ve paylaşın.",Price=1300, Stock=20, IsApproved=true,CategoryId=1,Image="2.jpg"},
                new Product() { Name = "Galaxy S8+",Description = "Galaxy S8+", Price = 700, Stock = 35, IsApproved = true, CategoryId = 3,Image="4.jpg"},
                new Product() { Name = "Samsung buzdolabi",Description = "Samsung buzdolabi", Price = 800, Stock = 40, IsApproved = true, CategoryId = 5,IsHome=true,Image="1.jpg"},
                new Product() { Name = "Asus RTX2080TI",Description = "Asus RTX2080TI", Price = 900, Stock = 45, IsApproved = false, CategoryId = 2,Image="2.jpg"},
                 new Product() { Name = "Msi Laptop",Description = "Msi Laptop", Price = 1000, Stock = 50, IsApproved = false, CategoryId = 2,IsHome=true,Image="3.jpg"},
                 new Product() { Name = "Intel Core I7-7700K",Description = "Intel Core I7-7700K", Price = 1100, Stock = 55, IsApproved = false, CategoryId = 2,IsHome=true,Image="4.jpg"}

            };
            foreach (var urun in urunler)
            {
                context.Products.Add(urun);
            }
            context.SaveChanges();
        }
    }
}

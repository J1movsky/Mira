using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mira.Domain.Entites;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;



namespace Mira.Domain
{
    public class AppDbContext : DbContext
    {   
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

        public DbSet<TextField> TextFields { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }

       

        

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            

            modelbuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "7526f485-ec2d-4ec8-bd73-12a7d1c49a5d",
                Name = "admin",
                NormalizedName= "ADMIN",
            });


            modelbuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "7526f485-ec2d-4ec8-bd73-12a7d1c49a5d",
                UserName= "admin",
                NormalizedUserName= "ADMIN",
                Email = "my@mail.com",
                NormalizedEmail = "ME@MAIL.COM",
                EmailConfirmed= true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "superpassword"),
                SecurityStamp = string.Empty
            });

            modelbuilder.Entity<IdentityUserRole<string>>().HasNoKey();

            modelbuilder.Entity<TextField>().HasData(new TextField 
            {
                Id =new Guid ("34bbc335-90de-0e39-3d1a-Ba394aDbc920"),
                CodeWord = "PageIndex",
                Title = "Главная"
            });

            modelbuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("12b3B3A5-3Aa5-102f-dc00-Dd394bD32f20"),
                CodeWord = "PageServeces",
                Title = "Наши услуги"
            }); 

            modelbuilder.Entity<TextField>().HasData(new TextField 
            {
                Id = new Guid("1d3f2d4a-93f1-102f-3d00-3Ae4adf5b2a6"),
                CodeWord = "PageContacts",
                Title = "Контакты"
            });

        }

    }
}

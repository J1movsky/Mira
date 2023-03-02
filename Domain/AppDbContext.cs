using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mira.Domain.Entites;
using System.Data.SqlTypes;

namespace Mira.Domain
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TextField> TextFields { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "936DA01F-9ABD-4d9d-80C7-02AF85C822A8",
                Name = "admin",
                NormalizedName= "ADMIN",
            });


            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "92DRT39DV-20SD-22dE-80DE-02AF85C822A8",
                UserName= "admin",
                NormalizedUserName= "ADMIN",
                Email = "my@mail.com",
                NormalizedEmail = "ME@MAIL.COM",
                EmailConfirmed= true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "superpassword"),
                SecurityStamp = string.Empty
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId= "34ggv33h5-90ui-0e39-3d00-UN394NDWK920",
                UserId= "92DRT39DV-20SD-22dE-80DE-02AF85C822A8"

            });

            builder.Entity<TextField>().HasData(new TextField 
            {
                Id =new Guid ("34rtv33h5-93uf-102f-3d00-Ud394NDWK920"),
                CodeWord = "PageIndex",
                Title = "Главная"
            });

            builder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("12tv33h5-3rt5-102f-dc00-Ud394ND32f20"),
                CodeWord = "PageServeces",
                Title = "Наши услуги"
            }); 

            builder.Entity<TextField>().HasData(new TextField 
            {
                Id = new Guid("1d3frw4t-93uf-102f-3d00-3we4rdf5gtr65fd"),
                CodeWord = "PageContacts",
                Title = "Контакты"
            });

        }

    }
}

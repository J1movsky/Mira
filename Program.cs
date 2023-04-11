using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Mira.Domain;
using Mira.Domain.Repositories.Abstract;
using Mira.Domain.Repositories.EntityFramevork;
using Mira.Service;


namespace Mira
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);


            // подключение конфигурацию из appseting.json
            var config = new Config();
            builder.Configuration.GetSection(Config.Project).Bind(config);


            // Подключаем нужный функционал приложения в качесте сервисов
            builder.Services.AddTransient<ITextFieldRepository, EFTextFieldRepository>();
            builder.Services.AddTransient<IServiceItemsRepository, EFServiceItemRepository>();
            builder.Services.AddTransient<DataManager>();

            // получаем строку подключения из файла конфигурации и  добавляем контекст ApplicationContext в качестве сервиса в приложение
            var connection = builder.Configuration["ConnectionStrings:DefaultConnection"];
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));



            
            // Добавляем сервисы для контроллера и представления (MVC) 
            builder.Services.AddControllersWithViews(x=>
            {
                x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea")); // для области адним (папки) передаем политику AdminArea, которая реализована ниже.
            });
            //Настраиваем политику авторизации для Admin area
            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); }); // требуем от пользователя роль admin
            });




            // Add services to the container.
            builder.Services.AddControllersWithViews();
            


            //Настраиваем Identity систему
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts=>
            {
                opts.User.RequireUniqueEmail =true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



            // Настраваем Authentication cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MiraAuth";
                options.Cookie.HttpOnly= true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath= "/account/accessdenied";
                options.SlidingExpiration = true;
            });




            var app = builder.Build();

            /// Порядок регистрации middleware очень важен


            // Подключение статичных файлов (css,js и т.д.)
            app.UseStaticFiles();


            app.UseHttpsRedirection();
            // подключаем систему машрутизации
            app.UseRouting();

            // Подключение авторизации
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseCookiePolicy();



            // Регистрация нужных маршрутов (ендпоинты)
            app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(name: "admin", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                 
                
            app.Run();
        }
    }
}
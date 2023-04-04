using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


            // получаем строку подключения из файла конфигурации
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            // добавляем контекст ApplicationContext в качестве сервиса в приложение
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));


            // Add services to the container.
            builder.Services.AddControllersWithViews();



            // подключение конфигурацию из appseting.json
            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            ///
            ///
            ///  СДЕЛАТЬ ЗАВТРА !!!!!!!!!! ////
            ///
            ///

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



            // Подключаем нужный функционал приложения
            builder.Services.AddTransient<ITextFieldRepository, EFTextFieldRepository>();
            builder.Services.AddTransient<IServiceItemsRepository, EFServiceItemRepository>();
            builder.Services.AddTransient<DataManager>();


            var app = builder.Build();


           


            app.UseHttpsRedirection();

            // Подключение статичных файлов (css,js и т.д.)
            app.UseStaticFiles();



            // подключаем систему машрутизации
            app.UseRouting();

            // Подключение авторизации
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseCookiePolicy();



            // Регистрация нужных маршрутов (ендпоинты)
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
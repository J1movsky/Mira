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

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // �������� ������ ����������� �� ����� ������������
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            // ��������� �������� ApplicationContext � �������� ������� � ����������
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));


            //����������� Identity �������
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts=>
            {
                opts.User.RequireUniqueEmail =true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



            // ���������� Authentication cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MiraAuth";
                options.Cookie.HttpOnly= true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath= "/account/accessdenied";
                options.SlidingExpiration = true;
            });


            // ����������� ������������ �� appseting.json
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            var section = config.GetSection(nameof(Project));
            var project = section.Get<Project>();


            // ���������� ������ ���������� ����������
            builder.Services.AddTransient<ITextFieldRepository, EFTextFieldRepository>();
            builder.Services.AddTransient<IServiceItemsRepository, EFServiceItemRepository>();
            builder.Services.AddTransient<DataManager>();


            var app = builder.Build();


            app.UseHttpsRedirection();

            // ����������� ��������� ������ (css,js � �.�.)
            app.UseStaticFiles();



            // ���������� ������� ������������
            app.UseRouting();

            // ����������� �����������
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseCookiePolicy();



            // ����������� ������ ��������� (���������)
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
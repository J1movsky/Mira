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


            // ����������� ������������ �� appseting.json
            var config = new Config();
            builder.Configuration.GetSection(Config.Project).Bind(config);


            // ���������� ������ ���������� ���������� � ������� ��������
            builder.Services.AddTransient<ITextFieldRepository, EFTextFieldRepository>();
            builder.Services.AddTransient<IServiceItemsRepository, EFServiceItemRepository>();
            builder.Services.AddTransient<DataManager>();

            // �������� ������ ����������� �� ����� ������������ �  ��������� �������� ApplicationContext � �������� ������� � ����������
            var connection = builder.Configuration["ConnectionStrings:DefaultConnection"];
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));



            
            // ��������� ������� ��� ����������� � ������������� (MVC) 
            builder.Services.AddControllersWithViews(x=>
            {
                x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea")); // ��� ������� ����� (�����) �������� �������� AdminArea, ������� ����������� ����.
            });
            //����������� �������� ����������� ��� Admin area
            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); }); // ������� �� ������������ ���� admin
            });




            // Add services to the container.
            builder.Services.AddControllersWithViews();
            


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




            var app = builder.Build();

            /// ������� ����������� middleware ����� �����


            // ����������� ��������� ������ (css,js � �.�.)
            app.UseStaticFiles();


            app.UseHttpsRedirection();
            // ���������� ������� ������������
            app.UseRouting();

            // ����������� �����������
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseCookiePolicy();



            // ����������� ������ ��������� (���������)
            app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(name: "admin", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                 
                
            app.Run();
        }
    }
}
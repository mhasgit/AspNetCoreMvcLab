using AspNetCoreMvcLab.Models;
using AspNetCoreMvcLab.Storage;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreMvcLab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StudentDbContext>(option =>
            {
                //builder.Configuration["ConnectionStrings:StudentsDbConnection"]
                //var connectionString = builder.Configuration.GetConnectionString("StudentsDbConnection");

                option.UseSqlServer(builder.Configuration.GetConnectionString("StudentsDbConnection"));
            });

            builder.Services.AddScoped<IStudentRepository, StudentRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // movlo.com:80/admin
            // movlo.com:80/admin/portal
            // movlo.com:80/admin/portal/dashborad
            // movlo.com:80/admin/portal/dashborad/123
            //app.MapControllerRoute(
            //    name: "Admin", // movlo.com:80/admin/portal/dashborad
            //    pattern: "Admin/{controller=Portal}/{action=Dashboard}/{id?}");

            // movlo.com:80/
            // movlo.com:80/home
            // movlo.com:80/home/index
            // movlo.com:80/home/index/123
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            StudentSeeder.Seed(app);

            app.Run();
        }
    }


    #region Another Method of Startup and Main
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // Add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // Configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class ProgramOld
    {
        public static void Main2(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
    #endregion
}
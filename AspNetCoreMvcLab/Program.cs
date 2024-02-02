using AspNetCoreMvcLab.Models;
using AspNetCoreMvcLab.Storage;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.Use(async (context, next) =>
            //{
            //    string userName = context.Request.Query["user"];
            //    if (userName != null)
            //    {
            //        var identity = new ClaimsIdentity("QueryTypeAuth");
            //        identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            //        var userPrincipal = new ClaimsPrincipal(identity);
            //        context.User = userPrincipal;
            //    }

            //    // This is where we called the next middleware in the request pipeline
            //    await next();
            //});

            //app.UseUserQueryAuth();

            app.UseStaticFiles();

            app.UseMiddleware<CustomAuthentication>();

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

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            //app.MapDefaultControllerRoute();

            app.UseEndpoints(endpoints =>
            {
                // Minimal APIs
                endpoints.MapGet("/signin", CustomAuth.SingIn);

                endpoints.MapGet("/signout", CustomAuth.SignOut);

                endpoints.MapDefaultControllerRoute();
            });

             StudentDbContextSeeder.Seed(app);
            app.Run();
        }
    }


    public static class CustomAuth
    {
        public async static Task SingIn(HttpContext context)
        {
            string userName = context.Request.Query["user"];
            if (userName != null)
            {
                context.Response.Cookies.Append("user", userName);
                await context.Response.WriteAsync($"User {userName} Authenticated");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }

        public async static Task SignOut(HttpContext context)
        {
            context.Response.Cookies.Delete("user");
            await context.Response.WriteAsync("Signed out");
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
            //if (!env.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

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
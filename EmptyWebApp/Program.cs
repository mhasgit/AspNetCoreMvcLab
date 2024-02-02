using Microsoft.Extensions.Options;

namespace EmptyWebApp
{

    /*
       "CountrySettings": {
        "UK": {
          "Timezone": "UTC",
          "HourFormat": "24"
        },
        "Pakistan": {
          "Timezone": "PKS",
          "HourFormat": "12"
        }
  },
     */
    public class CountrySettings
    {
        public CountrySetting UK { get; set; }

        public CountrySetting Pakistan { get; set; }
    }

    public class CountrySetting
    {
        public string Timezone { get; set; }
        public string HourFormat { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Service Registration

            var config = builder.Configuration;
            // builder.Environment
            builder.Services.Configure<CountrySettings>(config.GetSection("CountrySettings"));

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromSeconds(6);
                option.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            app.UseSession();

            app.UseHttpLogging();

            app.UseStaticFiles();


            // HTTP Request Pipeline setup

            // app.Configuration
            // app.Environment

            app.MapGet("/", () => "Hello World");

            app.Logger.LogInformation("config handler setting up");
            app.MapGet("config", async (HttpContext context, IConfiguration configuration, IOptions<CountrySettings> countrySettingsOption, IWebHostEnvironment hostEnvironment, ILogger<Program> logger) =>
            {
                logger.LogTrace("LogTrace:: Config handeler executing");
                logger.LogDebug("LogDebug:: Config handeler executing");
                logger.LogInformation("LogInformation:: Config handeler executing");
                logger.LogWarning("LogWarning:: Config handeler executing");

                var defaultLogLevel = configuration["Logging:LogLevel:Default"];

                var ukSettings = countrySettingsOption.Value.UK.Timezone;

                var environment = configuration["ASPNETCORE_ENVIRONMENT"];

                var testSetting = configuration["TestSetting"];

                var response = $"Logging:LogLevel:Default = {defaultLogLevel}";
                response += "</ br>";
                response += $"ASPNETCORE_ENVIRONMENT = {environment}";
                response += "</ br>";
                response += $"testSetting = {testSetting}";

                var environmentName = hostEnvironment.EnvironmentName;
                if (hostEnvironment.IsDevelopment())
                {

                }

                if (hostEnvironment.IsEnvironment("QA"))
                {

                }

                await context.Response.WriteAsync(response);
            });

            // Cookies
            app.MapGet("cookie", async context =>
            {
                var visitCookieCount = context.Request.Cookies["VisitCount"];
                if (visitCookieCount != null)
                {
                    var count = int.Parse(visitCookieCount) + 1;
                    context.Response.Cookies.Append("VisitCount", count.ToString(),
                        new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(30),
                        });
                    await context.Response.WriteAsync($"You have visited the site {count} time(s)");
                }
                else
                {
                    context.Response.Cookies.Append("VisitCount", "1",
                        new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(30),
                        });
                    await context.Response.WriteAsync($"You are a first time visitor");
                }
            });

            app.MapGet("clear", context =>
            {
                context.Response.Cookies.Delete("VisitCount");
                context.Response.Redirect("/");
                return Task.CompletedTask;
            });

            // Session and Cookie
            app.MapGet("sessionandcookie", async context =>
            {
                var visitorIdValue = context.Request.Cookies["VisitorId"];
                if(visitorIdValue != null)
                {
                    var sessionCountValue = context.Session.GetInt32(visitorIdValue);
                    var visitCount = (sessionCountValue ?? 0) + 1;
                    context.Session.SetInt32(visitorIdValue, visitCount);
                    await context.Session.CommitAsync();
                    await context.Response.WriteAsync($"You have visited the site {visitCount} time(s)");
                } else
                {
                    var visitorId = Guid.NewGuid().ToString();
                    context.Response.Cookies.Append("VisitorId", visitorId);
                    context.Session.SetInt32(visitorId, 1);
                    await context.Session.CommitAsync();
                    await context.Response.WriteAsync($"You are a first time visitor");
                }
            });

            app.MapGet("session", async context =>
            {
                var sessionCountValue = context.Session.GetInt32("VisitCount");
                if (sessionCountValue != null)
                {
                    var visitCount = sessionCountValue.Value + 1;
                    context.Session.SetInt32("VisitCount", visitCount);
                    await context.Session.CommitAsync();
                    await context.Response.WriteAsync($"You have visited the site {visitCount} time(s)");
                }
                else
                {
                    context.Session.SetInt32("VisitCount", 1);
                    await context.Session.CommitAsync();
                    await context.Response.WriteAsync($"You are a first time visitor");
                }
            });

            app.Run();
        }
    }
}
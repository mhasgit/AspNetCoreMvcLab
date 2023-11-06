using AspNetCoreMvcLab.Storage;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreMvcLab.Models
{
    public static class StudentSeeder
    {
        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StudentDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if(!context.Students.Any())
            {
                context.Students.AddRange(new[] {
                    new Student() { Name = "Test1", Email = "Test@emal.com", Phone = "07234234", FeePaid = true },
                    new Student() { Name = "Test2", Email = "Test@emal.com", Phone = "07234234", FeePaid = true },
                    new Student() { Name = "Test3", Email = "Test@emal.com", Phone = "07234234", FeePaid = true },
                    new Student() { Name = "Test4", Email = "Test@emal.com", Phone = "07234234", FeePaid = false },
                    new Student() { Name = "Test5", Email = "Test@emal.com", Phone = "07234234", FeePaid = true },
                    new Student() { Name = "Test6", Email = "Test@emal.com", Phone = "07234234", FeePaid = false },
                    new Student() { Name = "Test7", Email = "Test@emal.com", Phone = "07234234", FeePaid = true },
                    new Student() { Name = "Test8", Email = "Test@emal.com", Phone = "07234234", FeePaid = true }
                });

                context.SaveChanges();
            }
        }
    }
}

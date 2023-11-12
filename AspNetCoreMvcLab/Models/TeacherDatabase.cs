namespace AspNetCoreMvcLab.Models
{
    public class TeacherDatabase
    {
        private static List<Teacher> students = new();

        public static IEnumerable<Teacher> Students => students;

        static TeacherDatabase()
        {
            students.AddRange(new[] {
                new Teacher() { Id = 1, Name = "Test1", Email = "Test@emal.com",  Address = "Ittehad colony Peshawar", Phone = "07234234"},
                new Teacher() { Id = 2, Name = "Test2", Email = "Test@emal.com",  Address = "Ittehad colony Peshawar", Phone = "07234234"},
                new Teacher() { Id = 3, Name = "Test3", Email = "Test@emal.com",  Address = "Ittehad colony Peshawar", Phone = "07234234"},
                new Teacher() { Id = 4, Name = "Test4", Email = "Test@emal.com",  Address = "Ittehad colony Peshawar", Phone = "07234234" },
                new Teacher() { Id = 5, Name = "Test5", Email = "Test@emal.com",  Address = "Ittehad colony Peshawar", Phone = "07234234"},
                new Teacher() { Id = 6, Name = "Test6", Email = "Test@emal.com",  Address = "Ittehad colony Peshawar", Phone = "07234234" },
                new Teacher() { Id = 7, Name = "Test7", Email = "Test@emal.com",  Address = "Ittehad colony Peshawar", Phone = "07234234"},
                new Teacher() { Id = 8, Name = "Test8", Email = "Test@emal.com",  Address = "Ittehad colony Peshawar", Phone = "07234234"}
            });
        }

        public static int NextId() => students.Count + 1;

        public static void AddStudent(Teacher teacher)
        {
            students.Add(teacher);
        }
    }
}

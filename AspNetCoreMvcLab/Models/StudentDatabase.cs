namespace AspNetCoreMvcLab.Models
{
    public static class StudentDatabase
    {
        private static List<Student> students = new();

        public static IEnumerable<Student> Students => students;

        static StudentDatabase()
        {
            students.AddRange(new[] {
                new Student() { Id = 1, Name = "Test1", Email = "Test@emal.com", Phone = "07234234", FeePaid = true },
                new Student() { Id = 2, Name = "Test2", Email = "Test@emal.com", Phone = "07234234", FeePaid = true },
                new Student() { Id = 3, Name = "Test3", Email = "Test@emal.com", Phone = "07234234", FeePaid = true },
                new Student() { Id = 4, Name = "Test4", Email = "Test@emal.com", Phone = "07234234", FeePaid = false },
                new Student() { Id = 5, Name = "Test5", Email = "Test@emal.com", Phone = "07234234", FeePaid = true },
                new Student() { Id = 6, Name = "Test6", Email = "Test@emal.com", Phone = "07234234", FeePaid = false },
                new Student() { Id = 7, Name = "Test7", Email = "Test@emal.com", Phone = "07234234", FeePaid = true },
                new Student() { Id = 8, Name = "Test8", Email = "Test@emal.com", Phone = "07234234", FeePaid = true }
            });
        }

        public static int NextId() => students.Count + 1;

        public static void AddStudent(Student student)
        {
            students.Add(student);
        }
    }


}

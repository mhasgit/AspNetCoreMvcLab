namespace AspNetCoreMvcLab.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool FeePaid { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }

        public List<Student> GetStudents()
        {
            return new List<Student>()
            {
                new Student() { Id = 1, Name = "Test1", Email = "Test@emal.com", Phone = "07234234", FeePaid = true },
                new Student() { Id = 2, Name = "Test2", Email = "Test@emal.com", Phone = "07234234", FeePaid = true },
                new Student() { Id = 3, Name = "Test3", Email = "Test@emal.com", Phone = "07234234", FeePaid = true },
                new Student() { Id = 4, Name = "Test4", Email = "Test@emal.com", Phone = "07234234", FeePaid = false },
                new Student() { Id = 5, Name = "Test5", Email = "Test@emal.com", Phone = "07234234", FeePaid = true },
                new Student() { Id = 6, Name = "Test6", Email = "Test@emal.com", Phone = "07234234", FeePaid = false },
                new Student() { Id = 7, Name = "Test7", Email = "Test@emal.com", Phone = "07234234", FeePaid = true },
                new Student() { Id = 8, Name = "Test8", Email = "Test@emal.com", Phone = "07234234", FeePaid = true }
            };
        }
    }
}

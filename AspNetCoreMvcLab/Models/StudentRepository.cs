using AspNetCoreMvcLab.Storage;

namespace AspNetCoreMvcLab.Models
{
    public interface IStudentRepository
    {
        IQueryable<Student> Students { get; }
    }


    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _context;

        public StudentRepository(StudentDbContext context)
        {
            this._context = context;
        }

        public IQueryable<Student> Students => _context.Students;
    }
}

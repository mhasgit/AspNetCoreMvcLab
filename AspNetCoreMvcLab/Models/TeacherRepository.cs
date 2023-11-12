using AspNetCoreMvcLab.Storage;

namespace AspNetCoreMvcLab.Models
{
    public interface ITeacherRepository 
    {
        IQueryable<Teacher> Teachers { get; }
    }

    public class TeacherRepository : ITeacherRepository
    {
        public readonly StudentDbContext _context;

        public TeacherRepository(StudentDbContext context)
        {
            _context = context;
        }

        public IQueryable<Teacher> Teachers => _context.Teachers;
    }
}

using AspNetCoreMvcLab.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvcLab.Models
{
    public class StudentController : Controller
    {
        private readonly StudentDbContext context;

        public StudentController(StudentDbContext context)
        {
            this.context = context;
        }

        // GET: Student
        public ActionResult Index()
        {
            return View(this.context.Students.ToList());
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            // StudentDatabase.AddStudent(new Student());
            return View();
        }

        [HttpPost]
        public ActionResult CreateStudent(Student student)
        {
            //var form = this.Request.Form;

            //var name = form["Name"];
            //var phone = form["Form"];
            //var email = form["Email"];

            //student.Id = StudentDatabase.NextId();
            //StudentDatabase.AddStudent(student);

            this.context.Students.Add(student);
            this.context.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}

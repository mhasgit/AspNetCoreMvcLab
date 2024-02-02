using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvcLab.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Teacher Name is Zarooori")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Teacher Email is Zarooori")]
        // [EmailAddress]
        // [Display(Name = "Company Email")]
        // [MaxLength(255)]
        // [MinLength(5)]
        // [StringLength(255)]
        public string Email { get; set; }


        // [Range(10000, 300000)]
        // public int DesiredSalary { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number can not be empty")]
        public string Phone { get; set; }
    }
}
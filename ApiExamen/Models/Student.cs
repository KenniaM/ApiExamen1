using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiExamen1.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        
        public Course Course { get; set; }

        
    }
}

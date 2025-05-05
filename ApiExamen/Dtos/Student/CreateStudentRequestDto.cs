
using System.ComponentModel.DataAnnotations;

namespace ApiExamen.Dtos.Student
{
  public class CreateStudentRequestDto
  { 
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int CourseId { get; set; }

  }
}
using ApiExamen1.Models;

namespace ApiExamen.Dtos.Student
{
  public class StudentDto
  { 
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
  }
}
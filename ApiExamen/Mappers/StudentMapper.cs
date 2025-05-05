using ApiExamen1.Models;
using ApiExamen.Dtos.Student;

namespace ApiExamen.Mappers
{
  public static class StudentMapper
  {
    public static StudentDto ToDto(this Student StudentItem)
    {
      return new StudentDto
      {
        Id = StudentItem.Id,
        Name = StudentItem.Name,
        Email = StudentItem.Email,
        Phone = StudentItem.Phone,
        CourseId = StudentItem.CourseId,
        CourseName = StudentItem.Course?.Name 

      };
    }
     public static Student ToStudentFromCreateDto(this CreateStudentRequestDto createStudentRequest)
    {
      return new Student
      {
        Name = createStudentRequest.Name,
        Email = createStudentRequest.Email,
        Phone = createStudentRequest.Phone,
        CourseId = createStudentRequest.CourseId

      };
    }
  }
}
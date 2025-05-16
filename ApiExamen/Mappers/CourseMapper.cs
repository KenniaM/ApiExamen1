using ApiExamen.Dtos.Course;
using ApiExamen1.Models;

namespace ApiExamen.Mappers
{
  public static class CourseMapper
  {
    public static CourseDtos ToDto(this Course courseItem)
    {
      return new CourseDtos
      {
        Id = courseItem.Id,
        Name = courseItem.Name,
        Description = courseItem.Description,
        ImageUrl = courseItem.ImageUrl,
        Schedule = courseItem.Schedule,
        Professor = courseItem.Professor

      };
    }

    public static Course ToCourseFromCreateDto(this CreateCourseRequestDto createCourseRequest)
    {
      return new Course
      {
        Name = createCourseRequest.Name,
        Description = createCourseRequest.Description,
        Schedule = createCourseRequest.Schedule,
        Professor = createCourseRequest.Professor
      };
    }


    public static void UpdateCourseFromDto(this Course course, CreateCourseRequestDto createCourseRequest)
    {
      course.Name = createCourseRequest.Name;
      course.Description = createCourseRequest.Description;
      course.Schedule = createCourseRequest.Schedule;
      course.Professor = createCourseRequest.Professor;

     
    }

  }
}
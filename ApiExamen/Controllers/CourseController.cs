using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExamen1.Data;
using ApiExamen.Dtos.Course; // Add this using for the DTOs
using ApiExamen.Mappers;
using Microsoft.AspNetCore.Authorization;
using ApiExamen1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiExamen.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages");

        public CourseController(ApplicationDBContext context)
        {
            _context = context;
        }

        //Obtener todos los cursos
        [HttpGet]
        public async Task<IActionResult> GetAllCourse()
        {
            var courses = await _context.Courses.ToListAsync();


            return Ok(courses);
        }

        //endpoint obtener por curso
        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetByIdCourse([FromRoute] int courseId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound(new { message = "Curso no encontrado" });
            }

            return Ok(course.ToDto());
        }


        //endpoint crear un nuevo curso curso
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCourseRequestDto courseDto)
        {
            if (courseDto.File == null || courseDto.File.Length == 0)
                return BadRequest("No file uploaded.");

            var courseModel = courseDto.ToCourseFromCreateDto();
            await _context.Courses.AddAsync(courseModel);
            await _context.SaveChangesAsync();

            var fileName = courseModel.Id.ToString() + Path.GetExtension(courseDto.File.FileName);
            var filePath = Path.Combine(_imagePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await courseDto.File.CopyToAsync(stream);
            }

            courseModel.ImageUrl = fileName;
            _context.Courses.Update(courseModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByIdCourse), new { courseId = courseModel.Id }, courseModel.ToDto());
        }


        //endpoint actulizar curso
        [HttpPut("{courseId}")]

        public async Task<IActionResult> UpdateCourse(int courseId, [FromForm] UpdateCourseRequestDto courseDto)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
                return NotFound("Course not found.");


            course.Name = courseDto.Name;
            course.Description = courseDto.Description;
            course.Schedule = courseDto.Schedule;
            course.Professor = courseDto.Professor;


            if (courseDto.File != null && courseDto.File.Length > 0)
            {
                var fileName = course.Id.ToString() + Path.GetExtension(courseDto.File.FileName);
                var filePath = Path.Combine(_imagePath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await courseDto.File.CopyToAsync(stream);
                }

                course.ImageUrl = fileName;
            }

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

            return Ok(course.ToDto());
        }
       
        //endpoint eliminar un curso

        [HttpDelete]
        [Route("{courseId}")]
        public async Task<IActionResult> Delete([FromRoute] int courseId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(_course => _course.Id == courseId);
            if (course  == null)
            {
                return NotFound();
            }
            _context.Courses.Remove(course );

            await _context.SaveChangesAsync();

            return NoContent();
        }




    }

}
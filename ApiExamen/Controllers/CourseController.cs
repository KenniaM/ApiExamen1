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

            return Ok(new
            {
                message = "Cursos obtenidos correctamente",
                data = courses.Select(c => c.ToDto())
            });
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


            return Ok(new
            {
                message = "Curso obtenido correctamente",
                data = course.ToDto()
            });
        }

        //endpoint crear un nuevo curso curso

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromForm] CreateCourseRequestDto courseRequestDto)
        {
            if (courseRequestDto.File == null || courseRequestDto.File.Length == 0)
                return BadRequest("No file uploaded.");

            var course = courseRequestDto.ToCourseFromCreateDto();
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();


            var fileName = course.Id.ToString() + Path.GetExtension(courseRequestDto.File.FileName);
            var filePath = Path.Combine(_imagePath, fileName);


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await courseRequestDto.File.CopyToAsync(stream);
            }


            course.ImageUrl = fileName;
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByIdCourse), new { courseId = course.Id }, course.ToDto());
        }

        //endpoint actulizar curso
        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourse(int courseId, [FromForm] CreateCourseRequestDto courseRequestDto)
        {

            var course = await _context.Courses.FindAsync(courseId);

            if (course == null)
            {
                return NotFound($"Course with ID {courseId} not found.");
            }


            course.UpdateCourseFromDto(courseRequestDto);


            if (courseRequestDto.File != null && courseRequestDto.File.Length > 0)
            {

                var fileName = course.Id.ToString() + Path.GetExtension(courseRequestDto.File.FileName);
                var filePath = Path.Combine(_imagePath, fileName);


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await courseRequestDto.File.CopyToAsync(stream);
                }


                course.ImageUrl = fileName;
            }

            try
            {

                _context.Courses.Update(course);
                await _context.SaveChangesAsync();


                return Ok(new { message = "Curso actualizado correctamente", data = course.ToDto() });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Hubo un error al actualizar el curso", error = ex.Message });
            }
        }

        //endpoint eliminar un curso

        [HttpDelete]
        [Route("{courseId}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int courseId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);

            if (course  == null)
            {
                
                return NotFound(new { message = $"Evento con ID {courseId} no encontrado." });
            }

            try
            {
               
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();

                // Mensaje de éxito
                return Ok(new { message = $"Evento con ID {courseId} eliminado correctamente." });
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, new { message = "Hubo un error al eliminar el evento.", error = ex.Message });
            }
        }




    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExamen1.Data;
using ApiExamen.Dtos.Student; // Add this using for the DTOs
using ApiExamen.Mappers;
using Microsoft.AspNetCore.Authorization;
using ApiExamen1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiExamen.Controllers
{
  [Route("api/student")]
  [ApiController]
  public class StudentController : ControllerBase
  {
    private readonly ApplicationDBContext _context;

    public StudentController(ApplicationDBContext context)
    {
      _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      var student = await _context.Students.FirstOrDefaultAsync(u => u.Id == id);
      if (student == null)
      {
        return NotFound(new { message = "Estudiante no encontrado" });
      }
      return Ok(student.ToDto());
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentRequestDto studentDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        var studentModel = studentDto.ToStudentFromCreateDto();
        _context.Students.Add(studentModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = studentModel.Id }, studentModel.ToDto());
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "Error al crear el estudiante", error = ex.Message });
      }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var students = await _context.Students.Include(s => s.Course).ToListAsync();
      return Ok(students.Select(s => s.ToDto()));
    }

    
    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetByCourseId([FromRoute] int courseId)
    {
      var students = await _context.Students.Where(s => s.CourseId == courseId).ToListAsync();
      if (!students.Any())
      {
        return NotFound(new { message = "No se encontraron estudiantes para este curso" });
      }
      return Ok(students.Select(s => s.ToDto()));
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStudentRequestDto studentDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var student = await _context.Students.FindAsync(id);
      if (student == null)
      {
        return NotFound(new { message = "Estudiante no encontrado" });
      }

      try
      {
        student.Name = studentDto.Name;
        student.Email = studentDto.Email;
        student.Phone = studentDto.Phone;
        student.CourseId = studentDto.CourseId;

        await _context.SaveChangesAsync();

        return Ok(student.ToDto());
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "Error al actualizar el estudiante", error = ex.Message });
      }
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      var student = await _context.Students.FindAsync(id);
      if (student == null)
      {
        return NotFound(new { message = "Estudiante no encontrado" });
      }

      try
      {
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Estudiante eliminado exitosamente" });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "Error al eliminar el estudiante", error = ex.Message });
      }
    }
  }
}
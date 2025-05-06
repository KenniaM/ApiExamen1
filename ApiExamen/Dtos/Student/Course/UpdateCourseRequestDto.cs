using System.ComponentModel.DataAnnotations;

namespace ApiExamen.Dtos.Course
{

    public class UpdateCourseRequestDto
  {
    

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    public string? Name { get; set; }

    [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
    public string? Description { get; set; }

    public IFormFile? File { get; set; }

    [StringLength(100, ErrorMessage = "El horario no puede exceder los 100 caracteres.")]
    public string? Schedule { get; set; }

    [StringLength(100, ErrorMessage = "El nombre del profesor no puede exceder los 100 caracteres.")]
    public string? Professor { get; set; }

  }
  
}
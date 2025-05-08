using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiExamen1.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        
        public string? Description { get; set; }

        
       public string? ImageUrl { get; set; }


        public string? Schedule { get; set; }

        public string? Professor { get; set; }

        public List<Student>? Students { get; set; }
    }
}

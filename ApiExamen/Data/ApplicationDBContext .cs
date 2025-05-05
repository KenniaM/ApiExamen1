using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExamen1.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiExamen1.Data
{
  public class ApplicationDBContext : DbContext
  {
    public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }


    //public DbSet<Event> Events { get; set; }//una tabla de la base de datos
  }
}

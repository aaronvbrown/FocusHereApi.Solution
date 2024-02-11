using Microsoft.EntityFrameworkCore;

namespace FocusHereApi.Models
{
  public class FocusHereApiContext : DbContext
  {

    public DbSet<Student> Students { get; set; }

    public FocusHereApiContext(DbContextOptions<FocusHereApiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Student>()
          .HasData(
              new Student { StudentId = 1, Name = "John Doe", GradeLevel = 12, SchoolName = "Springfield High School" },
              new Student { StudentId = 2, Name = "Jane Doe", GradeLevel = 11, SchoolName = "Springfield High School" },
              new Student { StudentId = 3, Name = "Jim Doe", GradeLevel = 10, SchoolName = "Springfield High School" },
              new Student { StudentId = 4, Name = "Jill Doe", GradeLevel = 9, SchoolName = "Springfield High School" }
          );
    }
  }
}
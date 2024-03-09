using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FocusHereApi.Models;

namespace FocusHereApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CourseController : ControllerBase
  {
    private readonly FocusHereApiContext _db;
    public CourseController(FocusHereApiContext db)
    {
      _db = db;
    }

    // GET: api/Courses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> Get([FromQuery] StudentParameters studentParameters, string name, string teacher, string period)
    {
      IQueryable<Course> query = _db.Courses.AsQueryable();

      if (!string.IsNullOrEmpty(name))
      {
        query = query.Where(c => c.Name.Contains(name));
      }

      if (!string.IsNullOrEmpty(period) && int.TryParse(period, out int periodInt))
      {
        query = query.Where(c => c.Period == periodInt);
      }

      if (!string.IsNullOrEmpty(teacher))
      {
        query = query.Where(c => c.Teacher.Contains(teacher));
      }

      return await query
                      .ToListAsync();
    }

    // GET:  api/Courses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourse(int id)
    {
      Course course = await _db.Courses.FindAsync(id);
      if (course == null)
      {
        return NotFound();
      }
      return course;
    }

    // POST: api/Courses
    [HttpPost]
    public async Task<ActionResult<Course>> Post(Course course)
    {
      _db.Courses.Add(course);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, course);
    }

    // Put: api/Courses/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Student student)
    {
      if (id != student.StudentId)
      {
        return BadRequest();
      }
      _db.Students.Update(student);
      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!StudentExists(id)) //This is checking to see if the view for the student exists.  Is that needed for an API request?
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return NoContent();
    }

    private bool StudentExists(int id)
    {
      return _db.Students.Any(e => e.StudentId == id);
    }

    // // Delete: api/Students/5
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteStudent(int id)
    // {
    //   Student student = await _db.Students.FindAsync(id);
    //   if (student == null)
    //   {
    //     return NotFound();
    //   }
    //   _db.Students.Remove(student);
    //   await _db.SaveChangesAsync();
    //   return NoContent();
    // }


  }
}


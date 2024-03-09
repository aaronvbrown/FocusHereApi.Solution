using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FocusHereApi.Models;

namespace FocusHereApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CoursesController : ControllerBase
  {
    private readonly FocusHereApiContext _db;
    public CoursesController(FocusHereApiContext db)
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

    // PUT: api/Courses/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Course course)
    {
      if (id != course.CourseId)
      {
        return BadRequest();
      }
      _db.Courses.Update(course);
      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CourseExists(id))
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

    private bool CourseExists(int id)
    {
      return _db.Courses.Any(e => e.CourseId == id);
    }

    // DELETE: api/Students/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
      Course course = await _db.Courses.FindAsync(id);
      if (course == null)
      {
        return NotFound();
      }
      _db.Courses.Remove(course);
      await _db.SaveChangesAsync();
      return NoContent();
    }


  }
}


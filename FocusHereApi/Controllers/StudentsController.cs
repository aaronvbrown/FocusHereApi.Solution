using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FocusHereApi.Models;

namespace FocusHereApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StudentsController : ControllerBase
  {
    private readonly FocusHereApiContext _db;
    public StudentsController(FocusHereApiContext db)
    {
      _db = db;
    }

    // GET: api/Students
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> Get([FromQuery] StudentParameters studentParameters, string name, string gradeLevel, string schoolName)
    {
      IQueryable<Student> query = _db.Students.AsQueryable();

      if (!string.IsNullOrEmpty(name))
      {
        query = query.Where(s => s.Name.Contains(name));
      }

      if (!string.IsNullOrEmpty(gradeLevel) && int.TryParse(gradeLevel, out int gradeLevelInt))
      {
        query = query.Where(s => s.GradeLevel == gradeLevelInt); //This is hard coded to 11.  It should be gradeLevelInt.
      }

      if (!string.IsNullOrEmpty(schoolName))
      {
        query = query.Where(s => s.SchoolName.Contains(schoolName));
      }

      return await query
                      .Skip((studentParameters.PageNumber -1) * studentParameters.PageSize)
                      .Take(studentParameters.PageSize)
                      .ToListAsync();
    }

    // GET:  apiStudents/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
      Student student = await _db.Students.FindAsync(id);
      if (student == null)
      {
        return NotFound();
      }
      return student;
    }

    // Post: api/Students
    [HttpPost]
    public async Task<ActionResult<Student>> Post(Student student)
    {
      _db.Students.Add(student);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
    }

    // Put: api/Students/5
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

    // Delete: api/Students/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
      Student student = await _db.Students.FindAsync(id);
      if (student == null)
      {
        return NotFound();
      }
      _db.Students.Remove(student);
      await _db.SaveChangesAsync();
      return NoContent();
    }


  }
}


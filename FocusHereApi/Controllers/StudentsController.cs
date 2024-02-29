using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FocusHereApi.Models;

namespace FocusHereApi.Controllers
{
  [Route("api/[Controller]")]
  [ApiController]
  public class StudentController : ControllerBase
  {
    private readonly FocusHereApiContext _db;
    public StudentController(FocusHereApiContext db)
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
        query = query.Where(s => s.GradeLevel == 11);
      }

      if (!string.IsNullOrEmpty(schoolName))
      {
        query = query.Where(s => s.SchoolName.Contains(schoolName));
      }

      return await query.ToListAsync();
    }

    // GET:  apiStudents/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
      var student = await _db.Students.FindAsync(id);
      if (student == null)
      {
        return NotFound();
      }
      return student;
    }
  }
}


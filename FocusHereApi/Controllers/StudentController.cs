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

    //Get: api/Student trying to simplify to get past 400 error
   [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> Get([FromQuery] StudentParameters studentParameters, string name, string gradeLevel, string schoolName) 
    {
      List<Student> students = await _db.Students.ToListAsync();

      return students;
    }
    
    //GET: api/Student
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Student>>> Get([FromQuery] StudentParameters studentParameters, string name, string gradeLevel, string schoolName) 
    // {
    //   IQueryable<Student> query = _db.Students.AsQueryable();

    //   if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(gradeLevel) && string.IsNullOrEmpty(schoolName))
    //   {
    //     return await query.ToListAsync();
    //   }

    //   // Apply filters based on the provided parameters
    //   if (name != null)
    //   {
    //     query = query.Where(s => s.Name.Contains(name));
    //   }

    //   if (gradeLevel != null)
    //   {
    //     query = query.Where(s => s.GradeLevel == int.Parse(gradeLevel));
    //   }

    //   if (schoolName != null)
    //   {
    //     query = query.Where(s => s.SchoolName.Contains(schoolName));
    //   }

    //   return await query.ToListAsync();
    // }
  }
}
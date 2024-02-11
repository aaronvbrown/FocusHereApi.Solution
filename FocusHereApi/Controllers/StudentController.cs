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

    //GET: api/Student
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> Get([FromQuery] StudentParameters studentParameters, string name, string gradeLevel, string schoolName) 
    {
      IQueryable<Student> query = _db.Students.AsQueryable();
      return await query
        .ToListAsync();
    }
  }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FocusHereApi.Models;

namespace FocusHereApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StudentController : ControllerBase
  {
    private readonly FocusHereApiContext _db;
    public StudentController(FocusHereApiContext db)
    {
      _db = db;
    }

    // GET: api/Student
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Student>>> Get([FromQuery] StudentParameters, string name, string gradeLevel, string schoolName) 
    // {
    //   IQueryable<Student> query = _db.Students.AsQueryable();
    //   return await query
    //     .ToListAsync();
    // }
  }
}
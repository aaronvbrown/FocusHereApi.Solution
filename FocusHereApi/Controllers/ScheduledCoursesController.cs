using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FocusHereApi.Models;

namespace FocusHereApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ScheduledCoursesController : ControllerBase
  {
    private readonly FocusHereApiContext _db;
    public ScheduledCoursesController(FocusHereApiContext db)
    {
      _db = db;
    }

    // GET: api/ScheduledCourses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScheduledCourse>>> Get([FromQuery] StudentParameters studentParameters, string period)
    {
      IQueryable<ScheduledCourse> query = _db.ScheduledCourses.AsQueryable();

      if (!string.IsNullOrEmpty(period) && int.TryParse(period, out int periodInt))
      {
        query = query.Where(s => s.Period == periodInt);
      }

      return await query
                      .ToListAsync();
    }

    // GET:  api/ScheduledCourses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduledCourse>> GetScheduledCourse(int id)
    {
      ScheduledCourse scheduledCourse = await _db.ScheduledCourses.FindAsync(id);
      if (scheduledCourse == null)
      {
        return NotFound();
      }
      return scheduledCourse;
    }

    // POST: api/ScheduledCourses
    [HttpPost]
    public async Task<ActionResult<ScheduledCourse>> Post(ScheduledCourse scheduledCourse)
    {
      _db.ScheduledCourses.Add(scheduledCourse);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetScheduledCourse), new { id = scheduledCourse.ScheduledCourseId }, scheduledCourse);
    }
  }
}
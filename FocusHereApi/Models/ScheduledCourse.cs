namespace FocusHereApi.Models

{
  public class ScheduledCourse
  {
    public int ScheduledCourseId { get; set; }
    public int Period { get; set; }
    public List<Student> Students { get; set; }
    public List<Course> Courses { get; set; }
    
    }
}
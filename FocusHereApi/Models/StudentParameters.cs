namespace FocusHereApi.Models
{
  public class StudentParameters
  {
    const int maxPageSize = 3;
    public int PageNumber { get; set; } = 1;

    private int _pageSize = 3;
    public int PageSize
    {
      get
      {
        return _pageSize;
      }
      set
      {
        _pageSize = (value > maxPageSize) ? maxPageSize : value;
      }
    }
  }
}
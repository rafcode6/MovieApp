using System.Collections.Generic;

namespace MovieLibrary.Data.Entities;
public class MovieParameters
{
    const int maxPageSize = 20;

    public int PageNumber { get; set; } = 1;
    public string Title { get; set; }
    public List<int> CategoriesID { get; set; } = new();
    public decimal? MinImdb { set; get; }
    public decimal? MaxImdb { set; get; }
    private int _pageSize = 5;

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

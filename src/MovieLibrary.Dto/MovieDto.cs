using System.Collections.Generic;

namespace MovieLibrary.Dto;

public class MovieDto : IEntityDto
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public decimal ImdbRating { get; set; }

    public IList<MovieCategoryDto> MovieCategories { get; set; }
    = new List<MovieCategoryDto>();
}
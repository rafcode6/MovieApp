using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Dto;

public class MovieUpsertDto
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Year is required")]
    public int Year { get; set; }

    public decimal ImdbRating { get; set; }

    public IList<MovieCategoryUpsertDto> MovieCategories { get; set; }
    = new List<MovieCategoryUpsertDto>();
}

using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Dto;

public class CategoryUpsertDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
}

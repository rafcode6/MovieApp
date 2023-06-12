using MovieLibrary.Core.Repository.Base;
using MovieLibrary.Data.Entities;
using MovieLibrary.Dto;

namespace MovieLibrary.Core.Repository.Abstraction;

public interface IMovieService : IServiceBase<MovieDto, MovieUpsertDto, MovieParameters>
{

}
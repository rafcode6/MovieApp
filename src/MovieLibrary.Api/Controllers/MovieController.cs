using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Core.LoggerService;
using MovieLibrary.Core.Repository.Abstraction;
using MovieLibrary.Data.Entities;
using MovieLibrary.Dto;
using System;

namespace MovieLibrary.Api.Controllers;

[Route("/v1/MovieManagement")]
public class MovieController : Controller
{
    private ILoggerManager _logger;
    private IMovieService _service;

    public MovieController(ILoggerManager logger, IMovieService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAllMovies()
    {
        try
        {
            var movies = _service.FindAll();
            _logger.LogInfo($"Returned all movies from database.");
            return Ok(movies);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetAllMovies action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("/v1/Movie/Filter")]
    public IActionResult GetAllMoviesByParameters([FromQuery] MovieParameters movieParameters)
    {
        try
        {
            var movies = _service.FindByCondition(movieParameters);

            _logger.LogInfo($"Returned all movies from database by parameters.");
            return Ok(movies);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetAllMovies action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieById(int id)
    {
        try
        {
            var movie = _service.Find(id);
            if (movie is null)
            {
                _logger.LogError($"Movie with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            return Ok(movie);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetMovieById action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public IActionResult CreateMovie([FromBody] MovieUpsertDto movie)
    {
        try
        {
            if (movie is null)
            {
                _logger.LogError("Movie object sent from client is null.");
                return BadRequest("Movie object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid movie object sent from client.");
                return BadRequest("Invalid model object");
            }

            var createdMovie = _service.Create(movie);
            
            return Ok(createdMovie);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside CreateMovie action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody] MovieUpsertDto movie)
    {
        try
        {
            if (movie is null)
            {
                _logger.LogError("Movie object sent from client is null.");
                return BadRequest("Movie object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid movie object sent from client.");
                return BadRequest("Invalid model object");
            }

            var movieEntity = _service.Update(id, movie);
            if (movieEntity is null)
            {
                _logger.LogError($"Movie with id: {id}, hasn't been found in db.");
                return NotFound();
            }
            
            return Ok(movieEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside UpdateMovie action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        try
        {
            _service.Delete(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside DeleteMovie action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Core.LoggerService;
using MovieLibrary.Core.Repository.Abstraction;
using MovieLibrary.Data.Entities;
using MovieLibrary.Dto;
using System;

namespace MovieLibrary.Api.Controllers;

[Route("/v1/CategoryManagement")]
public class CategoryController : Controller
{
    private ILoggerManager _logger;
    private ICategoryService _service;

    public CategoryController(ILoggerManager logger, ICategoryService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAllCategories()
    {
        try
        {
            var categories = _service.FindAll();
            _logger.LogInfo($"Returned all categories from database.");
            return Ok(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetAllCategories action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("/v1/Category/Filter")]
    public IActionResult GetAllCategoriesByParameters([FromQuery] CategoryParameters categoryParameters)
    {
        try
        {
            var categories = _service.FindByCondition(categoryParameters);

            _logger.LogInfo($"Returned all categories from database by parameters.");
            return Ok(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetAllCategoriesByParameters action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetCategoryById(int id)
    {
        try
        {
            var category = _service.Find(id);
            if (category is null)
            {
                _logger.LogError($"Category with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            return Ok(category);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetCategoryById action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public IActionResult CreateCategory([FromBody] CategoryUpsertDto category)
    {
        try
        {
            if (category is null)
            {
                _logger.LogError("Category object sent from client is null.");
                return BadRequest("Category object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid category object sent from client.");
                return BadRequest("Invalid model object");
            }

            var createdCategory = _service.Create(category);

            return Ok(createdCategory);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside CreateCategory action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(int id, [FromBody] CategoryUpsertDto category)
    {
        try
        {
            if (category is null)
            {
                _logger.LogError("Category object sent from client is null.");
                return BadRequest("Category object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid category object sent from client.");
                return BadRequest("Invalid model object");
            }

            var categoryEntity = _service.Update(id, category);
            if (categoryEntity is null)
            {
                _logger.LogError($"Category with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            return Ok(categoryEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside UpdateCategory action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        try
        {
            _service.Delete(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside DeleteCategory action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}

﻿namespace FinancialManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CategoryPostDTO categoryPostDTO,
                                            [FromServices] IValidator<CategoryPostDTO> validator)
    {
        var validation = validator.Validate(categoryPostDTO);

        if (validation.IsValid is false)
        {
            return BadRequest(validation.Errors.Select(e => e.ErrorMessage));
        }

        var result = await categoryService.CreateAsync(categoryPostDTO);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await categoryService.RemoveAsync(id);

        if (result)
        {
            return NoContent();
        }

        return BadRequest("Category was not deleted");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await categoryService.GetByIdAsync(id);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await categoryService.GetCategoriesAsync();

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CategoryPutDTO category,
                                         [FromServices] IValidator<CategoryPutDTO> validator)
    {
        var validation = validator.Validate(category);

        if (validation.IsValid is false)
        {
            return BadRequest(validation.Errors.Select(e => e.ErrorMessage));
        }

        var wasUpdated = await categoryService.UpdateAsync(category);

        if (wasUpdated)
        {
            return NoContent();
        }

        return BadRequest("Category was not updated");
    }
}

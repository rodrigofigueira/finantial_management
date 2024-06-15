using FinancialManagement.Application.DTOs;
using FinancialManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CategoryPostDTO categoryPostDTO)
        {
            var result = await categoryService.CreateAsync(categoryPostDTO);

            if(result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }

    }
}

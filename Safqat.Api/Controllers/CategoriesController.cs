using MediatR;
using Microsoft.AspNetCore.Mvc;
using Safqat.Application.Categories.Commands.CreateCategory;
using Safqat.Application.Categories.Queries.GetAllCategories;

namespace Safqat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ISender sender) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCategoryCommand command,
            CancellationToken cancellationToken)
        {
            var id = await sender.Send(command, cancellationToken);

            return CreatedAtAction(
                nameof(Create),
                new { id },
                id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                new GetAllCategoriesQuery(),
                cancellationToken);

            return Ok(result);
        }
    }
}

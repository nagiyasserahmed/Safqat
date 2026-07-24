using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Safqat.Application.Safqat.Commands.ConfirmMedia;
using Safqat.Application.Safqat.Commands.CreateDraftSafqa;
using Safqat.Application.Safqat.Commands.PresignMedia;
using Safqat.Application.Safqat.Commands.PublishSafqa;
using Safqat.Application.Safqat.Commands.UpdateDraftSafqa;

namespace Safqat.Api.Controllers
{
    [ApiController]
    [Route("safqat")]
    public class SafqaController(ISender mediator) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateDraft([FromBody] CreateDraftSafqaCommand command, CancellationToken cancellationToken)
        {
            var safqaId = await mediator.Send(command, cancellationToken);
            return Ok(safqaId);
        }
        
        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateDraft([FromBody] UpdateDraftSafqaCommand req)
        {
           var updatedSafqa = await mediator.Send(req);
            
            return Ok(updatedSafqa);
        }

        [HttpPost("{id}/media/presign")]
        public async Task<IActionResult> PresignMedia(Guid id, [FromBody] PresignMediaCommand req)
        {
            var safqaMedia = await mediator.Send(req);

            return CreatedAtAction(nameof(PresignMedia), safqaMedia);
        }

        [HttpPost("media/confirm")]
        public async Task<IActionResult> ConfirmMedia([FromBody] ConfirmMediaCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{id}/publish")]
        public async Task<IActionResult> Publish(PublishSafqaCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}

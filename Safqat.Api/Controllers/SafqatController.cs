using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Safqat.Application.Safqat.Commands.CreateDraftSafqa;
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

        //    // Step 3: user adds a photo — SafqaId already exists from step 1
        //    [HttpPost("{id}/media/presign")]
        //    public async Task<IActionResult> PresignMedia(Guid id, [FromBody] PresignRequest req)
        //    {
        //        var safqa = await _repo.GetAsync(id);
        //        if (safqa is null) return NotFound();
        //        if (safqa.PublisherId != User.GetUserId()) return Forbid();

        //        var mediaId = Guid.NewGuid();
        //        var key = $"safqa/{id}/{mediaId}/original{Path.GetExtension(req.FileName)}";

        //        var url = _s3.GetPreSignedURL(new GetPreSignedUrlRequest
        //        {
        //            BucketName = Bucket,
        //            Key = key,
        //            Verb = HttpVerb.PUT,
        //            Expires = DateTime.UtcNow.AddMinutes(10),
        //            ContentType = req.ContentType
        //        });

        //        await _repo.AddMediaAsync(new SafqaMedia
        //        {
        //            Id = mediaId,
        //            SafqaId = id,
        //            Path = key,
        //            Type = req.ContentType.StartsWith("video") ? MediaType.Video : MediaType.Image,
        //            Status = MediaStatus.Pending
        //        });

        //        return Ok(new { mediaId, uploadUrl = url });
        //    }

        //    // Step 4: client confirms after the direct S3 PUT succeeds
        //    [HttpPost("{id}/media/{mediaId}/confirm")]
        //    public async Task<IActionResult> ConfirmMedia(Guid id, Guid mediaId)
        //    {
        //        var media = await _repo.GetMediaAsync(mediaId);
        //        if (media is null || media.SafqaId != id) return NotFound();

        //        media.Status = MediaStatus.Uploaded; // background job later flips this to Ready
        //        await _repo.SaveChangesAsync();
        //        return NoContent();
        //    }

        //    // Step 5: user taps "publish"
        //    [HttpPost("{id}/publish")]
        //    public async Task<IActionResult> Publish(Guid id)
        //    {
        //        var safqa = await _repo.GetWithMediaAsync(id);
        //        if (safqa is null) return NotFound();
        //        if (safqa.PublisherId != User.GetUserId()) return Forbid();

        //        try
        //        {
        //            safqa.Publish();
        //        }

        //        catch (InvalidOperationException ex)
        //        {
        //            return BadRequest(new { error = ex.Message });
        //        }

        //        await _repo.SaveChangesAsync();
        //        return NoContent();
        //    }
    }

    public record UpdateSafqaRequest(string Title, string Description, string Address, decimal Price, bool IsNegotiable);
    public record PresignRequest(string FileName, string ContentType);
}

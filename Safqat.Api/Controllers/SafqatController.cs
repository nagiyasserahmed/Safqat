using Microsoft.AspNetCore.Mvc;

namespace Safqat.Api.Controllers
{
    [ApiController]
    [Route("safqat")]
    public class SafqaController : ControllerBase
    {
        //    private readonly ISafqaRepository _repo;
        //    private readonly IAmazonS3 _s3;
        //    private const string Bucket = "safqat-media";

        //    public SafqaController(ISafqaRepository repo, IAmazonS3 s3)
        //    {
        //        _repo = repo;
        //        _s3 = s3;
        //    }

        //    // Step 1: user taps "create listing" — before filling anything in
        //[HttpPost]
        //public async Task<IActionResult> CreateDraft()
        //{
        //    var userId = User.GetUserId(); // from auth
        //    var safqa = Safqa.CreateDraft(Guid.NewGuid(), userId, categoryId: Guid.Empty);
        //    await _repo.AddAsync(safqa);
        //    return Ok(new { safqaId = safqa.Id });
        //}

        //    // Step 2: user fills the form, can call this repeatedly while editing
        //    [HttpPatch("{id}")]
        //    public async Task<IActionResult> UpdateDraft(Guid id, [FromBody] UpdateSafqaRequest req)
        //    {
        //        var safqa = await _repo.GetAsync(id);
        //        if (safqa is null) return NotFound();
        //        if (safqa.PublisherId != User.GetUserId()) return Forbid();

        //        safqa.UpdateDraft(req.Title, req.Description, req.Address, req.Price, req.IsNegotiable);
        //        await _repo.SaveChangesAsync();
        //        return NoContent();
        //    }

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

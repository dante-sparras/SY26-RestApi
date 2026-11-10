using AdsApi.Data;
using AdsApi.Dtos;
using AdsApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdsApi.Controllers;

[ApiController]
[Route("Ads")]
public class AdsController : ControllerBase
{
    private readonly AdsDbContext _db;

    public AdsController(AdsDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdResponseDto>>> GetAll()
    {
        var ads = await _db.Ads.ToListAsync();
        return Ok(ads.Select(ToResponse));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AdResponseDto>> GetById(int id)
    {
        var ad = await _db.Ads.FindAsync(id);
        return ad is null ? NotFound() : Ok(ToResponse(ad));
    }

    [HttpPost]
    public async Task<ActionResult<AdResponseDto>> Create(AdRequestDto request)
    {
        var ad = new Ad
        {
            Title = request.Title,
            Description = request.Description,
            Price = request.Price
        };

        _db.Ads.Add(ad);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = ad.Id }, ToResponse(ad));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AdRequestDto request)
    {
        var ad = await _db.Ads.FindAsync(id);
        if (ad is null)
        {
            return NotFound();
        }

        ad.Title = request.Title;
        ad.Description = request.Description;
        ad.Price = request.Price;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<AdRequestDto> patchDocument)
    {
        if (patchDocument is null)
        {
            return BadRequest();
        }

        var ad = await _db.Ads.FindAsync(id);
        if (ad is null)
        {
            return NotFound();
        }

        var request = new AdRequestDto
        {
            Title = ad.Title,
            Description = ad.Description,
            Price = ad.Price
        };

        patchDocument.ApplyTo(request, ModelState);
        if (!ModelState.IsValid || !TryValidateModel(request))
        {
            return ValidationProblem(ModelState);
        }

        ad.Title = request.Title;
        ad.Description = request.Description;
        ad.Price = request.Price;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ad = await _db.Ads.FindAsync(id);
        if (ad is null)
        {
            return NotFound();
        }

        _db.Ads.Remove(ad);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    private static AdResponseDto ToResponse(Ad ad)
    {
        return new AdResponseDto
        {
            Id = ad.Id,
            Title = ad.Title,
            Description = ad.Description,
            Price = ad.Price
        };
    }
}

using AdsApi.Data;
using AdsApi.Dtos;
using AdsApi.Models;
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

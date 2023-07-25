using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using receiptBook.Models;

namespace receiptBook.Controllers
{
    // RecipeController
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly ReceipeContext _context;

        public RatingController(ReceipeContext context)
        {
            _context = context;
        }
        // Get: api/Rating
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRating()
        {
            var rating = await _context.Ratings.ToListAsync();
            return Ok(rating);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        [HttpPost]
        public async Task<ActionResult<Rating>> SaveRating(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRating), new { id = rating.Id }, rating);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRating(int id, Rating rating)
        {
            if (id != rating.Id)
            {
                return BadRequest();
            }

            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Rating>>> SearchRating(string keywords)
        {
            var rating = await _context.Ratings
                .Where(r => r.Comment.Contains(keywords))
                .ToListAsync();

            return Ok(rating);
        }

        // Add other endpoints for rating, comments, images, usage dates, etc.

        // Helper method to check if a recipe exists
        private bool RatingExists(int id)
        {
            return _context.Ratings.Any(r => r.Id == id);
        }
    }

}

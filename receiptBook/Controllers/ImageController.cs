using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using receiptBook.Models;

namespace receiptBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly ReceipeContext _context;

        public ImageController(ReceipeContext context)
        {
            _context = context;
        }
        // Get: api/Rating
        [HttpGet("allImageForRecipe/{id}")]
        public async Task<ActionResult<IEnumerable<Image>>> GetImagesForRecipe(int id)
        {
            var images = await _context.Images.TakeWhile(i => i.RecipeId == id).ToArrayAsync();
            
            if (images == null)
            {
                return NotFound();
            }
            return Ok(images);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetImage(int id)
        {
            var image = await _context.Ratings.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }

        [HttpPost]
        public async Task<ActionResult<Image>> SaveImage(Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetImage), new { id = image.Id }, image);
        }

       

        // Add other endpoints for rating, comments, images, usage dates, etc.

        // Helper method to check if a recipe exists
        private bool RatingExists(int id)
        {
            return _context.Ratings.Any(r => r.Id == id);
        }
    }
}

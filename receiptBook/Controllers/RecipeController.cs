using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using receiptBook.Models;

namespace receiptBook.Controllers
{
    // RecipeController
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ReceipeContext _context;

        public RecipeController(ReceipeContext context)
        {
            _context = context;
        }
        // Get: api/Receipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            var recipes = await _context.Recipes.ToListAsync();
            foreach (var recipe in recipes)
            {
                recipe.Ingredients = await _context.Ingredients.Where(i => i.RecipeId == recipe.Id).ToListAsync();
            }
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

        [HttpPost]
        public async Task<ActionResult<Recipe>> CreateRecipe(RecipeCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipe = new Recipe
            {
                Name = model.Name,
                Description = model.Description,
                Ingredients = new List<Ingredient>() // Initialize the Ingredients list
            };

            // Add each ingredient provided by the user to the recipe's Ingredients list
            foreach (var ingredientName in model.Ingredients)
            {
                var ingredient = new Ingredient { IngName = ingredientName };
                _context.Ingredients.Add(ingredient);
                recipe.Ingredients.Add(ingredient);
            }

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
        }

        /*
        [HttpPost]
        public async Task<ActionResult<Recipe>> SaveRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
        }
        */
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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
        public async Task<ActionResult<IEnumerable<Recipe>>> SearchRecipes(string keywords)
        {
            var recipes = await _context.Recipes
                .Where(r => r.Name.Contains(keywords) || r.Description.Contains(keywords))
                .ToListAsync();

            return Ok(recipes);
        }

        // Add other endpoints for rating, comments, images, usage dates, etc.

        // Helper method to check if a recipe exists
        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(r => r.Id == id);
        }

        [HttpGet("byIngredient/{ingredientName}")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipesByIngredient(string ingredientName)
        {
            var recipes = await _context.Recipes
                .Include(r => r.Ingredients) // Include the Ingredients related to the Recipe
                .Where(r => r.Ingredients.Any(i => i.IngName.Contains(ingredientName)))
                .ToListAsync();
            
            if (recipes == null || recipes.Count == 0)
            {
                return NotFound();
            }

            return Ok(recipes);
        }
    }

}

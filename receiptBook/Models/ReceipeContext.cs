using Microsoft.EntityFrameworkCore;

namespace receiptBook.Models
{
    public class ReceipeContext : DbContext
    {
        public ReceipeContext(DbContextOptions<ReceipeContext> options ): base(options) { }
        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;

        public DbSet<UsageDate> UsageDates { get; set; } = null!;
    }
}

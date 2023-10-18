using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;

namespace receiptBook.Models
{
    // Recipe Model
    public class Recipe
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Description")]
        public string Description { get; set; }
        [JsonPropertyName("Ingredients")]
        public List<Ingredient> Ingredients { get; set; }
        /*public List<Rating> Ratings { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Image> Images { get; set; }
        public List<UsageDate> UsageDates { get; set; }*/
    }

    public class RecipeCreateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; } // List of ingredient names provided by the user
    }

    // Rating Model
    public class Rating
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Stars")]
        public int Stars { get; set; }

        [JsonPropertyName("Comment")]
        public string Comment { get; set; }

        [JsonPropertyName("RecipeId")]
        public int RecipeId { get; set; }
    }


    // Comment Model
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int RecipeId { get; set; }
    }

    // Image Model
    public class Image
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Url")]
        public string Url { get; set; }
        
        [JsonPropertyName("RecipeId")]
        public int RecipeId { get; set; }
    }

    // Usage Date Model
    public class UsageDate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int RecipeId { get; set; }
    }

    public class Ingredient
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("IngName")]
        public string IngName { get; set; }
        [JsonPropertyName("RecipeId")]
        public int ?RecipeId { get; set; } // Add this property


    }
}

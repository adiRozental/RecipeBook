using Newtonsoft.Json;
using receiptBook.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Markup;
using JsonSerializer = System.Text.Json.JsonSerializer;

public class RecipeService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl =    "https://localhost:7154/swagger/index.html"; // Replace with your API base URL

    public RecipeService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    public async Task<List<Recipe>> GetAllRecipes()
    {
        var response = await _httpClient.GetAsync("https://localhost:7154/api/recipe"); // Replace with the appropriate API endpoint
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
     
        var recipes = JsonSerializer.Deserialize<List<Recipe>>(content);

        return recipes;
    }
    
    public async Task<List<Recipe>> GetRecipesByKeyword(string str)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://localhost:7154/api/Recipe/search?keywords={str}\r\n"); // Replace with the appropriate API endpoint

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var recipes = JsonSerializer.Deserialize<List<Recipe>>(content);

            return recipes;

        }
        catch (HttpRequestException)
        {

            throw;
        }

        
    }

    public async Task SaveImage(Image image)
    {
       
            var json = JsonConvert.SerializeObject(image);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // 5. Send an HTTP POST request
            var response = await _httpClient.PostAsync("https://localhost:7154/api/image", content);

            // Check if the request was successful
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error: " + response.StatusCode);
            }
            
        
        
    }
   

    // Add more methods to handle other API endpoints (e.g., CreateRecipe, UpdateRecipe, DeleteRecipe, etc.)
}

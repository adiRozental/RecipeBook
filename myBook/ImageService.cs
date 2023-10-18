using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using receiptBook.Models;
using Newtonsoft.Json;

namespace myBook
{
    public class TagsResponse
    {
        [JsonProperty("result")]
        public Result Result { get; set; }

        public Status Status { get; set; }
    }

    public class Result
    {
        public List<TagItem> Tags { get; set; }
    }

    public class TagItem
    {
        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        [JsonProperty("tag")]
        public Tag Tag { get; set; }
    }

    public class Tag
    {
        [JsonProperty("en")]
        public string EnglishTag { get; set; }
    }

    public class Status
    {
        public string text { get; set; }
        public string type { get; set; }
    }
    public class ImageService
    {
        public async Task<bool> GetFoodImageUrlAsync(string ImageUrl, string foodName)
        {
            string apiKey = "acc_3c2f1817f3d310d";
            string apiSecret = "7f6189da58a80d449fdcf63a6691e1d8";
            string baseUrl = "https://api.imagga.com/v2/tags/";

            using (HttpClient client = new HttpClient())
            {
                // Set up the HTTP request headers for authentication
                string basicAuthValue = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", apiKey, apiSecret)));

                client.DefaultRequestHeaders.Add("Authorization", $"Basic {basicAuthValue}");

                // Send a GET request to Imagga API
                HttpResponseMessage response = await client.GetAsync($"{baseUrl}?image_url={ImageUrl}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    try
                    {
                        

                        // Deserialize the JSON response
                        TagsResponse apiResponse = JsonConvert.DeserializeObject<TagsResponse>(content);

                        // Extract the tags
                        List<string> tagsList = apiResponse.Result.Tags.ConvertAll(tagItem => tagItem.Tag.EnglishTag);

                        // Print the extracted tags
                       

                        // Check if the response contains the image URL
                        if (apiResponse != null && tagsList.Count>0)
                        {
                            return tagsList.Contains(foodName);
                        }
                        else
                        {
                            // Handle the case where the response doesn't contain a valid tag
                            throw new Exception("No tags found in the API response.");
                            
                        }
                    }
                    catch (JsonException ex)
                    {
                        // Handle JSON parsing error
                        throw new Exception("Error parsing JSON: " + ex.Message);
                        
                    }
                }
                else
                {
                    // Handle API error
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }
    }
    

  
}

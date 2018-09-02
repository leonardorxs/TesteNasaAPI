using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using TesteNasaAPI.Models;

namespace TesteNasaAPI.Pages
{
    public class IndexModel : PageModel
    {
        public string ImageDate { get; set; }

        public void OnGet([FromServices] IConfiguration config)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var baseUrl = config.GetSection("NASA_OpenAPIs:BaseURL").Value;
                var key = config.GetSection("NASA_OpenAPIs:Key").Value;
                var baseDate = DateTime.Now.Date.AddDays(new Random().Next(0, 7) * -1);
                var response = client.GetAsync(
                    $"{baseUrl}apod?api_key={key}&date={baseDate:yyyy-MM-dd}").Result;

                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;

                dynamic result = JsonConvert.DeserializeObject(content);

                var image = new NASAImage()
                {
                    Title = result.title,
                    Description = result.explanation,
                    Url = result.url,
                    MediaType = result.media_type,
                    Date = baseDate
                };

                TempData["NASAImage"] = image;

            }
        }
    }
}

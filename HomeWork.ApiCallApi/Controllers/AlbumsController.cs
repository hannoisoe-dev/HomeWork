using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace HomeWork.ApiCallApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly HttpClient _client = new HttpClient();

        private readonly string _endpoint = "https://jsonplaceholder.typicode.com/albums";
        [HttpGet]

        public async Task<IActionResult> GetAlbums()
        {
            HttpResponseMessage lst = await _client.GetAsync(_endpoint);
            if (lst.IsSuccessStatusCode)
            {
                string result = await lst.Content.ReadAsStringAsync();
                var response =JsonConvert.DeserializeObject<List<Album>>(result);
                return Ok(response);
            }
            return BadRequest("Not Found");

        }

    }


    public class Album
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
    }

}

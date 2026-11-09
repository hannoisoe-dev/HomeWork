using HomeWork.Domain.Feature.Blog;
using HomeWork.Domain.Feature.Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogEFCoreController : ControllerBase
    {
        private readonly BlogService _service = new BlogService();

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _service.getBlogs();
            if (lst.Count == 0)
            {
                return BadRequest("No Data Found!");
            }
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _service.getBlog(id);
            if (item is null)
            {
                return BadRequest("No Data Found!");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogCreateRequest request)
        {
            var response = _service.createBlog(request);
            return response.IsSuccess ? Ok(response.Message) : BadRequest(response.Message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogCreateRequest request)
        {
            var response = _service.updateBlog(request, id);
            return response.IsSuccess ? Ok(response.Message) : BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var response = _service.deleteBlog(id);
            return response.IsSuccess ? Ok(response.Message) : BadRequest(response.Message);
        }
    }
}


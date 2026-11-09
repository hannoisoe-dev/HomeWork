using HomeWork.Domain.Feature.Blog;
using HomeWork.Domain.Feature.Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly BlogDapperService _service = new BlogDapperService();

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _service.GetBlogs();
            if (lst.Count == 0)
            {
                return BadRequest("No Data Found!");
            }
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _service.GetBlog(id);
            if (item is null)
            {
                return BadRequest("No Data Found!");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogCreateRequest request)
        {
            var result = _service.CreateBlog(request);
            return result > 0 ? Ok("Blog created successfully.") : BadRequest("Creating blog failed.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogCreateRequest request)
        {
            int result = _service.UpdateBlog(id, request);
            return result > 0 ? Ok("Updated") : BadRequest("Update Fail");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            int result = _service.DeleteBlog(id);
            return result > 0 ? Ok("Deleted!") : BadRequest("Delete Fail.");
        }
    }
}


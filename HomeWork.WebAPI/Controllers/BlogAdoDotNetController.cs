using HomeWork.Domain.Feature.Blog;
using HomeWork.Domain.Feature.Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static HomeWork.Domain.Feature.Blog.BlogDapperService;

namespace HomeWork.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        private readonly BlogAdoDotNetService _service = new BlogAdoDotNetService();

        [HttpGet]

        public IActionResult GetBlogs()
        {
            DataTable dt=_service.GetBlogs();
            if(dt.Rows.Count == 0)
            {
                return BadRequest("Not Data Found!");
            }

            List<BlogModel> lst = new List<BlogModel>();


            foreach(DataRow dr in dt.Rows)
            {
                lst.Add(new BlogModel
                {
                    BlogId = Convert.ToInt32(dr["BlogId"]),
                    BlogTitle = dr["BlogTitle"].ToString(),
                    BlogAuthor = dr["BlogAuthor"].ToString(),
                    BlogContent = dr["BlogContent"].ToString()
                });
            }
            return Ok(lst);
        }


        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            DataTable dt =_service.GetBlog(id);
            if (dt.Rows.Count == 0)
            {
                return BadRequest("Not Data Found!");
            }

            List<BlogModel> blog=new List<BlogModel>();

            foreach(DataRow dr in dt.Rows)
            {
                blog.Add(new BlogModel
                {
                    BlogId = Convert.ToInt32(dr["BlogId"]),
                    BlogTitle = dr["BlogTitle"].ToString(),
                    BlogAuthor = dr["BlogAuthor"].ToString(),
                    BlogContent = dr["BlogContent"].ToString()
                });
            }

            return Ok(blog);

        }

        [HttpPost]

        public IActionResult createBlog(BlogCreateRequest request)
        {
            var lst=_service.createBlog(request);
            return Ok(lst);
        }

        [HttpPut("{id}")]

        public IActionResult updateBlog(int id,BlogCreateRequest request)
        {
            int lst = _service.updateBlog(id, request);
            return lst > 0 ? Ok("Updated") : BadRequest("Update Fail");
        }

        [HttpDelete("{id}")]

        public IActionResult deleteBlog(int id)
        {
            int result=_service.DeleteBlog(id);
            return result > 0 ? Ok("Deleted!") : BadRequest("Delete Fail.");
        }
    }
}

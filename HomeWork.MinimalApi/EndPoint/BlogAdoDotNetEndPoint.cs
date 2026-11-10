using HomeWork.Database.Models;
using HomeWork.Domain.Feature.Blog;
using HomeWork.Domain.Feature.Blog.Models;
using System.Data;
using static HomeWork.Domain.Feature.Blog.BlogDapperService;

namespace HomeWork.MinimalApi.EndPoint
{
    public static class BlogAdoDotNetEndPoint
    {
        public static void UseBlogAdoDotNetService(this IEndpointRouteBuilder app)
        {
            app.MapGet("/Blog", (BlogAdoDotNetService service) =>
            {
                DataTable dt = service.GetBlogs();
                List<TblBlog> blogs = new List<TblBlog>();

                foreach (DataRow item in dt.Rows)
                {
                    blogs.Add(new TblBlog
                    {
                        BlogId = Convert.ToInt32(item["BlogId"]),
                        BlogTitle = item["BlogTitle"].ToString(),
                        BlogAuthor = item["BlogAuthor"].ToString(),
                        BlogContent = item["BlogContent"].ToString()

                    });
                }
                return blogs;
            })
                .WithName("GetBlogs")
                .WithOpenApi();

            app.MapGet("/Blog/id", (int id, BlogService service) =>
            {
                TblBlog result = service.getBlog(id);

                if (result is not null)
                {
                    List<TblBlog> blog = new List<TblBlog>();
                    blog.Add(new TblBlog
                    {
                        BlogId = result.BlogId,
                        BlogTitle = result.BlogTitle,
                        BlogAuthor = result.BlogAuthor,
                        BlogContent = result.BlogContent
                    });
                    return blog;
                }
                return null;
            })
                .WithName("GetBlog")
                .WithOpenApi();


            app.MapPost("/Blog", (BlogCreateRequest request, BlogAdoDotNetService service) =>
            {
                BlogCreateResponse response = service.createBlog(request);
                return response.Message;
            })
                .WithName("CreateBlog")
                .WithOpenApi();

            app.MapPatch("/Blog", (int id, BlogCreateRequest request, BlogAdoDotNetService service) =>
            {
                int result = service.updateBlog(id, request);
                return result > 0 ? "Updated!" : "Updated Fail";
            })
                .WithName("UpdateBlog")
                .WithOpenApi();

            app.MapDelete("/Blog/id", (int id, BlogAdoDotNetService service) =>
            {
                int result = service.DeleteBlog(id);
                return result > 0 ? "Deleted!" : "Delete Fail";
            })
                .WithName("DeleteBlog")
                .WithOpenApi();

        }
    }
}

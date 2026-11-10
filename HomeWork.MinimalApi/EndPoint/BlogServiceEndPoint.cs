using HomeWork.Database.Models;
using HomeWork.Domain.Feature.Blog;
using HomeWork.Domain.Feature.Blog.Models;

namespace HomeWork.MinimalApi.EndPoint
{
    public  static class BlogServiceEndPoint
    {
        public static void UseBlogService(this IEndpointRouteBuilder app)
        {
            app.MapGet("/Blog", (BlogService service) =>
            {
                var lst = service.getBlogs();
                return lst is null ? null : lst;
            })
            .WithName("GetBlogs")
            .WithOpenApi();


            app.MapGet("/Blog/id", (int id, BlogService service) =>
            {
                TblBlog blog = service.getBlog(id);
                return blog is null ? null : blog;
            })
            .WithName("GetBlog")
            .WithOpenApi();


            app.MapPost("/Blog", (BlogCreateRequest request, BlogService service) =>
            {
                BlogCreateResponse response = service.createBlog(request);
                return response.Message;
            })
                .WithName("CreateBlog")
                .WithOpenApi();

            app.MapPatch("/Blog", (int id, BlogCreateRequest request, BlogService service) =>
            {
                BlogCreateResponse response = service.updateBlog(request, id);
                return response.Message;
            })
                .WithName("UpdateBlog")
                .WithOpenApi();

            app.MapDelete("/Blog", (int id, BlogService service) =>
            {
                BlogCreateResponse response = service.deleteBlog(id);
                return response.Message;
            })
                .WithName("DeleteBlog")
                .WithOpenApi();
        }

            
    }
}

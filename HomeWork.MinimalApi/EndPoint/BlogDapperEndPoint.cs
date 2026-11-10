using HomeWork.Domain.Feature.Blog;
using HomeWork.Domain.Feature.Blog.Models;

namespace HomeWork.MinimalApi.EndPoint
{
    public static class BlogDapperEndPoint
    {

        public static void UseBlogDapperService(this IEndpointRouteBuilder app)
        {
            app.MapGet("/Blog", (BlogDapperService service) =>
            {
                var lst = service.GetBlogs();
                return lst is null ? null : lst;
            })
                .WithName("GetBlogs")
                .WithOpenApi();

            app.MapGet("/Blog/id", (int id, BlogDapperService service) =>
            {
                var lst = service.GetBlog(id);
                return lst is null ? null : lst;
            })
                 .WithName("GetBlog")
                 .WithOpenApi();

            app.MapPost("/Blog", (BlogDapperService service, BlogCreateRequest request) =>
            {
                int result = service.CreateBlog(request);
                return result > 0 ? "Created!" : "Create Fail";

            })
                .WithName("CreateBlog")
                .WithOpenApi();

            app.MapPatch("/Blog", (int id, BlogDapperService service, BlogCreateRequest request) =>
            {
                int result = service.UpdateBlog(id, request);
                return result > 0 ? "Updated!" : "Update Fail";
            })
                .WithName("UpdateBlog")
                .WithOpenApi();

            app.MapDelete("/Blog", (int id, BlogDapperService service) =>
            {
                int result = service.DeleteBlog(id);
                return result > 0 ? "Deleted!" : "Delete Fail";
            })
                .WithName("DeleteBlog")
                .WithOpenApi();
        }
    }
}

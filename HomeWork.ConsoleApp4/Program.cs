// See https://aka.ms/new-console-template for more information
using HomeWork.Domain.Feature.Blog;
using HomeWork.Domain.Feature.Blog.Models;

Console.WriteLine("Hello, World!");

BlogService blog = new BlogService();
//var lst=blog.getBlogs();
//foreach(var item in lst)
//{
//    Console.WriteLine(item.BlogTitle);
//}

//var lst=blog.getBlog(1);
//Console.WriteLine(lst.BlogTitle);

//BlogCreateResponse result=blog.createBlog(new BlogCreateRequest
//{
//    Title = "Fighting",
//    Author = "Hns",
//    Content = "Fighting...."
//});
//Console.WriteLine( result.IsSuccess +" " + result.Message);


//blog.updateBlog(new BlogCreateRequest
//{
//    Author = "haha"
//}, 29);


BlogCreateResponse result=blog.deleteBlog(29);
Console.WriteLine(result.Message);
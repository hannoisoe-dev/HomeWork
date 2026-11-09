// See https://aka.ms/new-console-template for more information
using HomeWork.Domain.Feature.Blog;
using HomeWork.Domain.Feature.Blog.Models;
using System.Data;
using System.Net.Http.Headers;

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


//BlogCreateResponse result=blog.deleteBlog(29);
//Console.WriteLine(result.Message);


BlogAdoDotNetService service = new BlogAdoDotNetService();
//DataTable lst = service.GetBlogs();
//foreach (DataRow dr in lst.Rows)
//{
//    Console.WriteLine(dr["BlogTitle"]);
//}

//DataTable lst = service.GetBlog(30);
//foreach (DataRow item in lst.Rows)
//{
//    Console.WriteLine(item["BlogTitle"]);
//}


//BlogCreateResponse result=service.createBlog(new BlogCreateRequest
//{
//    Title = "November",
//    Author = "HNS",
//    Content = "Winter is coming"
//});
//Console.WriteLine(result.IsSuccess + " " + result.Message);


//int result=service.updateBlog(30, new BlogCreateRequest
//{
//    Title = "Game of Throne"
//});
//if(result > 0)
//{
//    Console.WriteLine("Updated!");
//}

//int result=service.DeleteBlog(30);
//if(result > 0)
//{
//    Console.WriteLine("Deleted!");
//}

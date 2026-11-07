using HomeWork.Database.Models;
using HomeWork.Domain.Feature.Blog.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Domain.Feature.Blog
{
    public class BlogService
    {
        public List<TblBlog> getBlogs()
        {
            AppDbContext db = new AppDbContext();
            return db.TblBlogs.Where(x=>x.DeleteFlag == false).ToList();
           
        }

        public TblBlog getBlog(int id)
        {
            AppDbContext db = new AppDbContext();
            return db.TblBlogs.Where(x => x.BlogId == id & x.DeleteFlag == false).FirstOrDefault();
        }

        public BlogCreateResponse createBlog(BlogCreateRequest request)
        {
            AppDbContext db = new AppDbContext();
            db.TblBlogs.Add(new TblBlog
            {
                BlogTitle = request.Title,
                BlogAuthor = request.Author,
                BlogContent = request.Content,
                DeleteFlag = false
            });
            int result= db.SaveChanges();
            //if(result > 0)
            //{
            //    return new BlogCreateResponse
            //    {
            //        IsSuccess = true,
            //        Message = "Created!"
            //    };
            //}

            return result > 0 ? new BlogCreateResponse
            {
                IsSuccess = true,
                Message = "Created!"
            } : new BlogCreateResponse
            {
                IsSuccess = false,
                Message = "Create Failed!"
            };
        }

        public BlogCreateResponse updateBlog(BlogCreateRequest request, int id) 
        {
            AppDbContext db = new AppDbContext();
            var lst= db.TblBlogs.Where(x=>x.BlogId == id & x.DeleteFlag==false).FirstOrDefault();
           if(lst is null)
            {
                return new BlogCreateResponse
                {
                    IsSuccess = false,
                    Message = "Not Found!"
                };
            }


            if (!request.Title.IsNullOrEmpty())
            {
                lst.BlogTitle = request.Title;
            }
            if (!request.Author.IsNullOrEmpty())
            {
                lst.BlogAuthor = request.Author;
            }
            if (!request.Content.IsNullOrEmpty())
            {
                lst.BlogContent = request.Content;
            }

            int result=db.SaveChanges();

            return result > 0 ? new BlogCreateResponse
            {
                IsSuccess = true,
                Message = "Updated!"
            } : new BlogCreateResponse
            {
                IsSuccess = false,
                Message = "Update Fail"
            };
        }

        public BlogCreateResponse deleteBlog(int id)
        {
            AppDbContext db = new AppDbContext();
            var lst=db.TblBlogs.Where(x => x.BlogId == id).FirstOrDefault();

            if(lst is null)
            {
                return new BlogCreateResponse
                {
                    IsSuccess = false,
                    Message = "Not Found!"
                };
            }

            lst.DeleteFlag = true;
            int result=db.SaveChanges();

            return result > 0 ? new BlogCreateResponse
            {
                IsSuccess = true,
                Message = "Deleted!"
            } : new BlogCreateResponse
            {
                IsSuccess = false,
                Message = "Delete Failed!"
            };
        }
    }
}

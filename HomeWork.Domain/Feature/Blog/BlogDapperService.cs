using Dapper;
using HomeWork.Domain.Feature.Blog.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Domain.Feature.Blog
{
    public class BlogDapperService
    {
        private readonly AppSetting _appSetting = new AppSetting();

        public List<BlogModel> GetBlogs()
        {
            string query = "select * from Tbl_Blog where  DeleteFlag=0";
            using IDbConnection db = new SqlConnection(_appSetting.connectionString);
            var lst = db.Query<BlogModel>(query).ToList();
            return lst;
        }

        public BlogModel GetBlog(int id)
        {
            string query = "select * from Tbl_Blog where BlogId=@BlogId and " +
                            "DeleteFlag=0";
            using IDbConnection db = new SqlConnection(_appSetting.connectionString);
            var lst = db.QueryFirstOrDefault<BlogModel>(query, new BlogModel
            {
                BlogId = id
            });
            return lst;
        }

        public int CreateBlog(BlogCreateRequest request)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
                               ([BlogTitle]
                               ,[BlogAuthor]
                               ,[BlogContent]
                               ,[DeleteFlag])
                         VALUES
                               (@BlogTitle
                               ,@BlogAuthor
                               ,@BlogContent
                               ,{0})";
            using IDbConnection db = new SqlConnection(_appSetting.connectionString);
            int result = db.Execute(query, new BlogCreateRequest
            {
                BlogTitle = request.BlogTitle,
                BlogAuthor = request.BlogAuthor,
                BlogContent = request.BlogContent

            });

            return result;
        }

        public int UpdateBlog(int id, BlogCreateRequest request)
        {
            string selectQuery = "select * from Tbl_Blog where BlogId=@BlogId and DeleteFlag=0";
            using IDbConnection db = new SqlConnection(_appSetting.connectionString);
            var lst = db.QueryFirstOrDefault<BlogModel>(selectQuery, new BlogModel
            {
                BlogId = id
            });

            if (lst is null)
            {
                return 0;
            }


            string condition = string.Empty;

            if (!string.IsNullOrEmpty(request.BlogTitle))
            {
                condition += "[BlogTitle]=@BlogTitle,";
            }
            if (!string.IsNullOrEmpty(request.BlogAuthor))
            {
                condition += "[BlogAuthor]=@BlogAuthor,";
            }
            if (!string.IsNullOrEmpty(request.BlogContent))
            {
                condition += "[BlogContent]=@BlogContent,";
            }

            condition = condition.Substring(0, condition.Length - 1);

            string query = $@"UPDATE [dbo].[Tbl_Blog]
                           SET {condition}
                              ,[DeleteFlag] = {0}
                         WHERE BlogId=@BlogId";

            if (!string.IsNullOrEmpty(request.BlogTitle))
            {
                lst.BlogTitle = request.BlogTitle;
            }
            if (!string.IsNullOrEmpty(request.BlogAuthor))
            {
                lst.BlogAuthor = request.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(request.BlogContent))
            {
                lst.BlogContent = request.BlogContent;
            }

            return db.Execute(query, lst);

        }

        public int DeleteBlog(int id)
        {
            string selectQuery = "select * from Tbl_Blog where BlogId=@BlogId and DeleteFlag=0";
            using IDbConnection db = new SqlConnection(_appSetting.connectionString);
            var lst = db.QueryFirstOrDefault<BlogModel>(selectQuery, new BlogModel
            {
                BlogId = id
            });

            if (lst is null)
            {
                return 0;
            }

            string query = "update Tbl_Blog set DeleteFlag=1 where BlogId=@BlogId";
            return db.Execute(query, new BlogModel
            {
                BlogId = id
            });

        }

        public class BlogModel
        {
            public int BlogId { get; set; }
            public string? BlogTitle { get; set; }
            public string? BlogAuthor { get; set; }
            public string? BlogContent { get; set; }
        }



    }
}

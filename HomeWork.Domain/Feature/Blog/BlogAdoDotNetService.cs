using HomeWork.Database.Models;
using HomeWork.Domain.Feature.Blog.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Domain.Feature.Blog
{
    public class BlogAdoDotNetService()
    {
        private readonly AppSetting _appSetting=new AppSetting();

        public DataTable GetBlogs()
        {
            SqlConnection connection = new SqlConnection(_appSetting.connectionString);
            String query = "select * from Tbl_Blog where DeleteFlag=0";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public DataTable GetBlog(int id)
        {
            SqlConnection connection = new SqlConnection(_appSetting.connectionString);
            string query = "select * from Tbl_Blog where BlogId=@BlogId and DeleteFlag=0";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;

        }

        public BlogCreateResponse createBlog(BlogCreateRequest request)
        {
            SqlConnection connection = new SqlConnection(_appSetting.connectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                           ([BlogTitle]
                           ,[BlogAuthor]
                           ,[BlogContent]
                           ,[DeleteFlag])
                     VALUES
                           (@BlogTitle
                           ,@BlogAuthor
                           ,@BlogContent
                           ,@DeleteFlag)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle",request.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor",request.Author);
            cmd.Parameters.AddWithValue("@BlogContent", request.Content);
            cmd.Parameters.AddWithValue("@DeleteFlag", 0);
            int result=cmd.ExecuteNonQuery();
            connection.Close();
            if(result > 0)
            {
                return new BlogCreateResponse
                {
                    IsSuccess = true,
                    Message = "Created!"
                };
                
            }

            return new BlogCreateResponse
            {
                IsSuccess = false,
                Message = "Create Failed!"
            };

            
        }

        public int updateBlog(int id,BlogCreateRequest request)
        {
            string condition = string.Empty;

            SqlConnection connection = new SqlConnection(_appSetting.connectionString);
            connection.Open();
            string selectQuery = "select * from Tbl_Blog where BlogId=@BlogId and DeleteFlag=0";
            SqlCommand cmd = new SqlCommand(selectQuery, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if(dt is null)
            {
                return 0;
            }

            if (!string.IsNullOrEmpty(request.Title))
            {
                condition += "[BlogTitle]=@BlogTitle,";
            }
            if (!string.IsNullOrEmpty(request.Author))
            {
                condition += "[BlogAuthor]=@BlogAuthor,";
            }
            if (!string.IsNullOrEmpty(request.Content))
            {
                condition += "[BlogContent]=@BlogContent,";
            }
            
            condition = condition.Substring(0, condition.Length - 1);
            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blog]
                           SET {condition}
                              ,[DeleteFlag] = @DeleteFlag
                         WHERE BlogId=@BlogId";
            cmd = new SqlCommand(query, connection);

            if (!string.IsNullOrEmpty(request.Title))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", request.Title);
            }
            if (!string.IsNullOrEmpty(request.Author))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", request.Author);
            }
            if (!string.IsNullOrEmpty(request.Content))
            {
                cmd.Parameters.AddWithValue("@BlogContent", request.Content);
            }

            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@DeleteFlag", 0);
            int res= cmd.ExecuteNonQuery();
            connection.Close();
            return res;

        }

        public int DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(_appSetting.connectionString);
            connection.Open();
            string query = "update Tbl_Blog set DeleteFlag=1 where BlogId=@BlogId";
            SqlCommand cmd = new SqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result= cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }

    }
}

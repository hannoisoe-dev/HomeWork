using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Domain.Feature.Blog.Models
{
    public class BlogCreateRequest
    {
        public string Title { get; set; }

        public string Author  { get; set; }

        public string Content { get; set; }

       


    }


    public class BlogCreateResponse
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}

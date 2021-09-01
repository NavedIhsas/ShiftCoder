using System;

namespace BlogManagement.Application.Contract.Article
{
    public class ArticleViewModel
   {
       public string Title { get; set; }
       public string Picture { get; set; }
       public string PublishDate { get; set; }
       public string CreationDate { get; set; }
       public long CategoryId { get; set; }
       public string CategoryName { get; set; }
       public bool IsPublish { get; set; }
        public long Id { get; set; }
    }
}

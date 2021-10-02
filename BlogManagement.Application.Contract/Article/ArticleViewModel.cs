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
        public string Slug { get; set; }
        public long BloggerId { get; set; }
        public string BloggerName { get; set; }
        public bool IsPublish { get; set; }
        public long Id { get; set; }
    }
}
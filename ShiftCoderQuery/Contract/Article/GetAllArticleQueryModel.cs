using System;

namespace ShiftCoderQuery.Contract.Article
{
    public class GetAllArticleQueryModel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Picture { get; set; }
        public string PictureTitle { get; set; }
        public string PictureAtl { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public DateTime CreationDate { get; internal set; }
    }
}
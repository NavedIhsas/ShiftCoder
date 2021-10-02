using System;
using System.Collections.Generic;

namespace ShiftCoderQuery.Contract.Article
{
    public class LatestArticleQueryModel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public int? VisitCount { get; set; }
        public string Picture { get; set; }
        public string PictureTitle { get; set; }
        public string PictureAtl { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }

        public DateTime CreationDate { get; set; }
        public long Id { get; internal set; }
        public List<CommentManagement.Domain.CourseCommentAgg.Comment> Comments { get; set; }
        public long BloggerId { get; internal set; }
        public string BloggerName { get; internal set; }
    }
}
using System;
using System.Collections.Generic;
using AccountManagement.Domain.Account.Agg;
using ShiftCoderQuery.Contract.Comment;

namespace ShiftCoderQuery.Contract.Article
{
    public class SinglePageArticleQueryModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int ShowOrder { get; set; }
        public string Picture { get; set; }
        public string PictureTitle { get; set; }
        public string PictureAtl { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public List<string> KeyWords { get; set; }
        public string MetaDescription { get; set; }
        public string CanonicalAddress { get; set; }
        public string BloggerName { get; set; }
        public string BloggerPicture { get; set; }
        public long BloggerId { get; set; }
        public string BloggerBio { get; set; }
        public string BloggerResume { get; set; }
        public string BloggerSkills { get; set; }
        public bool IsPublish { get; set; }
        public int? VisitCount { get; set; }
        public string PublishDate { get; set; }
        public string CategoryName { get; set; }
        public List<CommentQueryModel> Comments { get; set; }
        public List<CommentManagement.Domain.CourseCommentAgg.Comment> CommentList { get; set; }
        public long Id { get; set; }
        public List<BloggerArticlesViewModel> BloggerArticlesList { get; internal set; }
        public DateTime CreationDate { get; internal set; }
    }

    public class BloggerArticlesViewModel
    {
        public string Title { get; set; }
        public string Slug { get; set; }
    }
}
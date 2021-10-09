using System;
using CommentManagement.Domain.VisitAgg;

namespace ShiftCoderQuery.Contract.Forum.Question
{
    public class QuestionQueryModel
    {
        public long Id { get; set; }
        public long? AccountId { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string AccountPicture { get; set; }
        public string CourseName { get; set; }
        public int NumberOfVisit { get; set; }
        public string CourseSlug { get; set; }
        public int AnswerCount { get; set; }
        public AnswerPagination Pagination { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
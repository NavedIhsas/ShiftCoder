using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Application;
using AccountManagement.Domain.Account.Agg;
using CommentManagement.Domain.VisitAgg;
using CommentManagement.Infrastructure.EfCore;
using Ganss.XSS;
using ShiftCoderQuery.Contract.Forum.Answer;
using ShiftCoderQuery.Contract.Forum.Question;
using ShopManagement.Domain.CourseAgg;
using ShopManagement.Domain.ForumAgg;
using ShopManagement.Infrastructure.EfCore;

namespace ShiftCoderQuery.Query
{
    public class ForumQuery : IForumQuery
    {
        private readonly ShopContext _context;
        private readonly ICourseRepository _course;
        private readonly IVisitRepository _visit;
        private static CommentContext _comment;
        public ForumQuery(ShopContext context, IAccountRepository account, ICourseRepository course, IVisitRepository visit, CommentContext commnet)
        {
            _context = context;
            _course = course;
            _visit = visit;
            _comment = commnet;
        }

        public long AddQuestion(AddQuestionQueryModel command)
        {
            var sanitizer = new HtmlSanitizer();
            var body = sanitizer.Sanitize(command.Body);

            var question = new Question(command.CourseId, command.AccountId, body, command.Title, command.Name,
                command.Slug.Slugify(), command.IsTrue);
            _context.Questions.Add(question);
            _context.SaveChanges();
            return question.Id;
        }

        public QuestionPagination QuestionCourse(long id, int pageId = 1)
        {
            var courseName = _course.GetCourseBy(id).Name;
            var courseSlug = _course.GetCourseBy(id).Slug;
            var questions = _context.Questions.Where(x => x.CourseId == id)
                .Select(x => new QuestionQueryModel
                {
                    Title = x.Title,
                    Id = x.Id,
                    AnswerCount = x.Answers.Count,
                    CourseName = courseName,
                    NumberOfVisit = MapVisit(x.Id),
                    CourseSlug = courseSlug
                }).ToList();

            const int take = 12;
            var skip = (pageId - 1) * take;

            var paging = new QuestionPagination
            {
                CurrentPage = pageId,
                PageCount = (int)Math.Ceiling(questions.Count / (double)take),
                Questions = questions.Skip(skip).Take(take).ToList()
            };
            return paging;
           
        }
        
        
        private static int MapVisit(long id)
        {
            var visit= _comment.Visits
                .Count(x => x.Type == ThisType.ShowQuestion && x.RecordOwnerId == id);
            return visit;
        }

        public QuestionQueryModel ShowQuestion(long questionId, string ipAddress, int pageId)
        {
           
               
            var question= _context.Questions.Select(x => new QuestionQueryModel
            {
                Title = x.Title,
                Id = x.Id,
                AccountId = x.AccountId,
                Body = x.Body,
                Name = x.Name,
                Pagination = MapAnswer(x.Answers, pageId),
                CreationDate = x.CreationDate
            }).FirstOrDefault(x => x.Id == questionId);

            var visit = _visit.GetVisitBy(ipAddress, ThisType.ShowQuestion, questionId);
            if (visit != null && visit.LastVisitDateTime.Date != DateTime.Now.Date)
            {
                visit.ReduceVisit(visit.NumberOfVisit);
                visit.SetDateTime();
                _visit.SaveChanges();
            }
            else if(visit==null)
            {
                var addVisit = new Visit(ThisType.ShowQuestion, ipAddress, DateTime.Now, 1, questionId);
                _visit.Create(addVisit);
                _visit.SaveChanges();
            }

            return question;
        }

        public void AddAnswer(AddAnswerQueryModel command)
        {
            var sanitizer = new HtmlSanitizer();
            var body = sanitizer.Sanitize(command.Body);

            var answer = new Answer(command.QuestionId, command.AccountId, body, command.Name);
            _context.Answers.Add(answer);
            _context.SaveChanges();
        }


        private static AnswerPagination MapAnswer(IEnumerable<Answer> answer, int pageId = 1)
        {
            var an = answer.Select(x => new AnswerQueryModel
            {
                Name = x.Name,
                Body = x.Body,
                CreationDate = x.CreationDate,
                QuestionId = x.QuestionId
            }).ToList();

            const int take = 6;
            var skip = (pageId - 1) * take;

            var paging = new AnswerPagination
            {
                PageCount = (int)Math.Ceiling(an.Count / (double)take),
                CurrentPage = pageId,
                Answers = an.OrderByDescending(x => x.CreationDate).Skip(skip).Take(take).ToList()
            };
            return paging;
        }
    }
}
using System;
using System.Collections.Generic;
using _0_FrameWork.Domain;
using ShopManagement.Domain.CourseAgg;

namespace ShopManagement.Domain.ForumAgg
{
   public class Question:EntityBase
    {
        public Question(long courseId, long? accountId, string body, string title,string name, string slug, bool isTrue)
        {
            CourseId = courseId;
            AccountId = accountId;
            Body = body;
            Title = title;
            Name = name;
            Slug = slug;
            IsTrue = isTrue;
            ModifiedDate = DateTime.Now;
        }

        public Question(string slug, bool isTrue)
        {
            Slug = slug;
            IsTrue = isTrue;
        }
        public long CourseId { get;private set; }
        public long? AccountId { get;private set; }
        public string Body { get; private set; }
        public string Title { get;private set; }
        public string Name { get; private set; }
        public string Slug { get;private set; }
        public bool IsTrue { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public List<Answer> Answers { get;private set; }
        public Course Course { get;private set; }
    }
}

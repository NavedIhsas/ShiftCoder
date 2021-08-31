using System;
using _0_FrameWork.Domain;
using ShopManagement.Domain.CourseAgg;

namespace ShopManagement.Domain.CourseEpisodeAgg
{
   public class CourseEpisode:EntityBase
    {
        public string FileName { get;private set; }
        public TimeSpan Time { get; private set; }
        public string Title { get; private set; }
        public bool IsFree { get; private set; }
        public string KeyWords { get;private set; }
        public string MetaDescription { get;private set; }
        public long CourseId { get; private set; }
        public Course Course { get; private set; }

        public CourseEpisode(string fileName, TimeSpan time, string title, long courseId,bool isFree, string keyWords, string metaDescription)
        {
            FileName = fileName;
            Time = time;
            Title = title;
            CourseId = courseId;
            IsFree = isFree;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
        }

        public void Edit(string fileName, TimeSpan time, string title, long courseId, bool isFree, string keyWords, string metaDescription)
        {
            if(!string.IsNullOrWhiteSpace(fileName))
              FileName = fileName;
            Time = time;
            Title = title;
            CourseId = courseId;
            IsFree = isFree;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
        }

    }
}


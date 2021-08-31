using System;

namespace Shop.Management.Application.Contract.CourseEpisode
{
    public class CourseEpisodeViewModel
    {
        public string FileName { get; set; }
        public TimeSpan Time { get; set; }
        public string Title { get; set; }
        public bool IsFree { get; set; }
        public string CourseName { get; set; }
        public long CourseId { get; set; }
        public TimeSpan TotalSpan { get; set; }
        public long Id { get; set; }
    }
}
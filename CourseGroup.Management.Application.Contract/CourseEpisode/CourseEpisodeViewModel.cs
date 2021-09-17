using System;
using Microsoft.AspNetCore.Http;

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
        public double TotalSpan { get; set; }
        public IFormFile File { get; set; }
        public long Id { get; set; }
    }
}
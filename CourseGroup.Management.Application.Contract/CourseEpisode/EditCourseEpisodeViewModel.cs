namespace Shop.Management.Application.Contract.CourseEpisode
{
    public class EditCourseEpisodeViewModel : CreateCourseEpisodeViewModel
    {
        public long Id { get; set; }
        public string FileName { get; set; }
    }
}
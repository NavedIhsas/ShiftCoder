namespace Shop.Management.Application.Contract.CourseGroup
{
    public class CourseGroupViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsRemove { get; set; }
        public string CreationDate { get; set; }
        public int CourseCount { get; set; }
        public string SubGroup { get; set; }
        public long? SubGroupId { get; set; }


    }
}
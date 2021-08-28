using _0_FrameWork.Domain;

namespace ShopManagement.Domain.CourseGroupAgg
{
   public class CourseGroup:EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRemove { get; set; }
        public string KeyWords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }

        public CourseGroup(string title, string description,  string keyWords, string metaDescription, string slug)
        {
            Title = title;
            Description = description;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            Slug = slug;
            IsRemove = false;
        }

        public void Edit(string title, string description, string keyWords, string metaDescription, string slug)
        {
            Title = title;
            Description = description;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        public void Remove()
        {
            IsRemove = true;
        }

        public void Restore()
        {
            IsRemove = false;
        }
    }
}

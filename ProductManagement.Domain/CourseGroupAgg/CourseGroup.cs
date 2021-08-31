using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using _0_FrameWork.Domain;

namespace ShopManagement.Domain.CourseGroupAgg
{
   public class CourseGroup:EntityBase
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsRemove { get; private set; }
        public string KeyWords { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public long? SubGroupId { get; private set; }
        public CourseGroup SubGroup { get; private set; }
        public List<CourseGroup> Groups { get; set; }
        public CourseGroup(string title, string description,  string keyWords, string metaDescription, string slug, long? subGroupId)
        {
            Title = title;
            Description = description;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            Slug = slug;
            SubGroupId = subGroupId;
            IsRemove = false;
        }

        public void Edit(string title, string description, string keyWords, string metaDescription, string slug, long? subGroupId)
        {
            Title = title;
            Description = description;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            Slug = slug;
            SubGroupId = subGroupId;
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

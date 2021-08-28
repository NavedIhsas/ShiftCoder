using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftCoderQuery.Contract.CourseGroup
{
   public class CourseGroupQueryModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRemove { get; set; }
        public string KeyWords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
    }
}

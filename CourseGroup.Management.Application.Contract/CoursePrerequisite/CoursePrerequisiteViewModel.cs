using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Management.Application.Contract.CoursePrerequisite
{
   public class CoursePrerequisiteViewModel
   {
       public long Id { get; set; }
       public string Title { get; set; }
       public bool IsRemove { get; set; }
       public string CourseName { get; set; }

   }
}

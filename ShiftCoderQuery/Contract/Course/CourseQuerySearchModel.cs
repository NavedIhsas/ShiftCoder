using System;
using System.Collections.Generic;

namespace ShiftCoderQuery.Contract.Course
{
    public class CourseQuerySearchModel
    {
       
        public string Name { get; set; }
        public double Price { get; set; }
        public string CreationDate { get; set; }
        public string Filter { get; set; }

    }
}
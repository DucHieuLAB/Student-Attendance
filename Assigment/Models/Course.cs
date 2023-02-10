using System;
using System.Collections.Generic;

#nullable disable

namespace Assigment.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseDetails = new HashSet<CourseDetail>();
        }

        public string CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public DateTime CourseStartDate { get; set; }
        public DateTime CourseEndDate { get; set; }
        public int NumOfSlot { get; set; }

        public virtual ICollection<CourseDetail> CourseDetails { get; set; }
    }
}

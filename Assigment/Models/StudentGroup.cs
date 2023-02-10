using System;
using System.Collections.Generic;

#nullable disable

namespace Assigment.Models
{
    public partial class StudentGroup
    {
        public StudentGroup()
        {
            CourseDetails = new HashSet<CourseDetail>();
            StudentGroupDetails = new HashSet<StudentGroupDetail>();
        }

        public string StudentGroupId { get; set; }
        public string StudentGroupName { get; set; }

        public virtual ICollection<CourseDetail> CourseDetails { get; set; }
        public virtual ICollection<StudentGroupDetail> StudentGroupDetails { get; set; }
    }
}

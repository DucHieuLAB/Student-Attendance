using System;
using System.Collections.Generic;

#nullable disable

namespace Assigment.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            CourseDetails = new HashSet<CourseDetail>();
        }

        public string TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastNamne { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Gmail { get; set; }
        public string Image { get; set; }

        public virtual ICollection<CourseDetail> CourseDetails { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Assigment.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentGroupDetails = new HashSet<StudentGroupDetail>();
        }

        public string Rollnumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }

        public virtual ICollection<StudentGroupDetail> StudentGroupDetails { get; set; }
    }
}

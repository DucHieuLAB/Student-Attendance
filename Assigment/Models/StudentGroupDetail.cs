using System;
using System.Collections.Generic;

#nullable disable

namespace Assigment.Models
{
    public partial class StudentGroupDetail
    {
        public StudentGroupDetail()
        {
            SlotAttendances = new HashSet<SlotAttendance>();
        }

        public string StudentGroupId { get; set; }
        public string StudentId { get; set; }

        public virtual Student Student { get; set; }
        public virtual StudentGroup StudentGroup { get; set; }
        public virtual ICollection<SlotAttendance> SlotAttendances { get; set; }
    }
}

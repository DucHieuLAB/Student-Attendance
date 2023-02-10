using System;
using System.Collections.Generic;

#nullable disable

namespace Assigment.Models
{
    public partial class CourseDetail
    {
        public CourseDetail()
        {
            SlotInformations = new HashSet<SlotInformation>();
        }

        public string CourseId { get; set; }
        public string SlotId { get; set; }
        public string TeacherId { get; set; }
        public string StudentGroupId { get; set; }

        public virtual Course Course { get; set; }
        public virtual StudentGroup StudentGroup { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<SlotInformation> SlotInformations { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Assigment.Models
{
    public partial class SlotInformation
    {
        public SlotInformation()
        {
            SlotAttendances = new HashSet<SlotAttendance>();
        }

        public string SlotId { get; set; }
        public bool? SlotStatus { get; set; }
        public string SlotNote { get; set; }
        public DateTime SlotDate { get; set; }
        public int SlotTimeName { get; set; }

        public virtual CourseDetail Slot { get; set; }
        public virtual SlotTime SlotTimeNameNavigation { get; set; }
        public virtual ICollection<SlotAttendance> SlotAttendances { get; set; }
    }
}

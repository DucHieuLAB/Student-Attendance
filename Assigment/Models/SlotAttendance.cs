using System;
using System.Collections.Generic;

#nullable disable

namespace Assigment.Models
{
    public partial class SlotAttendance
    {
        public string SlotId { get; set; }
        public string StudentGroupId { get; set; }
        public string StudentId { get; set; }
        public bool Attendance { get; set; }
        public string Note { get; set; }
        public DateTime SlotDate { get; set; }
        public int SlotTimeName { get; set; }
        public SlotAttendance()
        {

        }

        public SlotAttendance(string slotId, string studentGroupId, string studentId, bool attendance, string note, DateTime slotDate, int slotTimeName)
        {
            SlotId = slotId;
            StudentGroupId = studentGroupId;
            StudentId = studentId;
            Attendance = attendance;
            Note = note;
            SlotDate = slotDate;
            SlotTimeName = slotTimeName;
        }
        public virtual SlotInformation Slot { get; set; }
        public virtual StudentGroupDetail Student { get; set; }
    }
}

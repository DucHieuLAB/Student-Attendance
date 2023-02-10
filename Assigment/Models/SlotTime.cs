using System;
using System.Collections.Generic;

#nullable disable

namespace Assigment.Models
{
    public partial class SlotTime
    {
        public SlotTime()
        {
            SlotInformations = new HashSet<SlotInformation>();
        }

        public int SlotTimeName { get; set; }
        public string SlotDate { get; set; }
        public string EndTime { get; set; }

        public virtual ICollection<SlotInformation> SlotInformations { get; set; }
    }
}

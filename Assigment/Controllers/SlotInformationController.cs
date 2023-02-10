using Assigment.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assigment.Controllers
{
    public class SlotInformationController : Controller
    {
        public IActionResult SlotInformation()
        {
            List<string> slotIds = new List<string>();
            List<int> SlotNameID = new List<int>();
            using (var context = new SchoolDatabaseContext())
            {
                slotIds = context.CourseDetails.Select(c => c.SlotId).ToList();
                SlotNameID = context.SlotTimes.Select(s => s.SlotTimeName).ToList();
            }
            ViewData["SlotID"] = slotIds;
            ViewData["SlotNameID"] = SlotNameID;

            return View();
        }
        public IActionResult DoCreateSlotInformation(SlotInformation Newslot)
        {
            
            using (var context = new SchoolDatabaseContext())
            {
                context.SlotInformations.Add(Newslot);
                
                string STUDENT_GROUP_ID = context.CourseDetails.FirstOrDefault(c => c.SlotId == Newslot.SlotId).StudentGroupId;
                List<Student> students = context.StudentGroupDetails.Where(s => s.StudentGroupId == STUDENT_GROUP_ID).Select(s => s.Student).ToList() ;
                foreach (Student s in students) {
                    context.SlotAttendances.Add(new SlotAttendance(Newslot.SlotId, STUDENT_GROUP_ID, s.Rollnumber, false, "", Newslot.SlotDate, Newslot.SlotTimeName));
                }
                context.SaveChanges();
            }
            return RedirectToAction("SlotInformation");
        }

        public IActionResult List(SlotInformation Newslot)
        {

            return View();
        }
    }
}

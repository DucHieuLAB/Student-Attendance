using Assigment.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assigment.Controllers
{
    public class GroupController : Controller
    {
        public IActionResult GroupDetail(string groupID)
        {
            List<Student> students = new List<Student>();
            ViewData["GroupID"] = groupID;
            using (var context = new SchoolDatabaseContext())
            {
                List<StudentGroupDetail> StudentGroupDetail = context.StudentGroupDetails.Where(sg => sg.StudentGroupId == groupID).ToList();
                foreach(StudentGroupDetail sgd in StudentGroupDetail)
                {
                    Student student = context.Students.FirstOrDefault(s => s.Rollnumber == sgd.StudentId);
                    students.Add(student);
                }
            }
                return View(students);
        }
        public IActionResult List(string SlotID,int SlotTimeName,DateTime SlotDate)
        {
            SlotInformation slot = new SlotInformation();
            using (var context = new SchoolDatabaseContext())
            {
                slot = context.SlotInformations.FirstOrDefault(s => s.SlotId == SlotID && s.SlotTimeName == SlotTimeName && s.SlotDate == SlotDate);
                slot.Slot = context.CourseDetails.FirstOrDefault(cd => cd.SlotId == SlotID);
                slot.Slot.Teacher = context.Teachers.FirstOrDefault(t => t.TeacherId == slot.Slot.TeacherId);
                slot.SlotAttendances = context.SlotAttendances.Where(sa => sa.SlotId == SlotID && sa.SlotDate == slot.SlotDate && sa.SlotTimeName == slot.SlotTimeName).ToList();
            }
                return View(slot);
        }
    }
}

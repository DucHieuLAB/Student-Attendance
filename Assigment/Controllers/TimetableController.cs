using Assigment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assigment.Controllers
{
    public class TimetableController : Controller
    {
        public IActionResult ScheduleOfWeek()
        {
            string studentid = this.HttpContext.Session.GetString("STUDENT_ID");
            if(studentid.Equals(string.Empty)) return RedirectToAction(actionName: "LoginRegister", controllerName: "Login");
            List<SlotAttendance> slotAttendances = null;
            using (var context = new SchoolDatabaseContext()) {
                slotAttendances = context.SlotAttendances.Where(s => s.StudentId == studentid).ToList();
                foreach (SlotAttendance s in slotAttendances) {
                    s.Slot = context.SlotInformations.FirstOrDefault(si => si.SlotId == s.SlotId && si.SlotTimeName == s.SlotTimeName && s.SlotDate.Equals(si.SlotDate));
                    s.Slot.Slot = context.CourseDetails.FirstOrDefault(si => si.SlotId == s.SlotId);
                }
                ViewData["StudentName"] = context.Students.FirstOrDefault(x => x.Rollnumber == studentid).FirstName + " " + context.Students.FirstOrDefault(x => x.Rollnumber == studentid).LastName;
            }

            return View(slotAttendances);
        }
        private bool CompareTime(SlotAttendance s)
        {
            bool Status = false;
            Status = DateTime.Compare(s.SlotDate, new DateTime(2022, 05, 09)) >= 0 ? true : false;
            if (Status)
            {
                Status = DateTime.Compare(s.SlotDate, new DateTime(2022, 05, 15)) <= 0 ? true : false;
            }
            return Status;
        }
        public IActionResult ScheduleOfWeekTeacher() {
            string date = "09/05/15/05";
            string[] dates = date.Trim().Split("/");
            DateTime start = new DateTime(2022, Convert.ToInt32(dates[1]), Convert.ToInt32(dates[0]));
            DateTime end = new DateTime(2022, Convert.ToInt32(dates[3]), Convert.ToInt32(dates[2]));
            ViewData["Start"] = start;
            ViewData["End"] = end;
            string TEACHER_ID = this.HttpContext.Session.GetString("TEACHER_ID");
            if (TEACHER_ID == null) return RedirectToAction(actionName: "Login", controllerName: "LoginRegister");
            List<CourseDetail> courses = null;
            using (var context = new SchoolDatabaseContext())
            {
                string teacherName = context.Teachers.FirstOrDefault(c => c.TeacherId == TEACHER_ID).FirstName + " " + context.Teachers.FirstOrDefault(c => c.TeacherId == TEACHER_ID).LastNamne;
                @ViewData["TeacherName"] = teacherName;

                courses = context.CourseDetails.Where(c => c.TeacherId == TEACHER_ID).ToList();
                foreach (CourseDetail c in courses)
                {
                    c.SlotInformations = context.SlotInformations.Where(s => s.SlotId == c.SlotId && s.SlotDate >= start && s.SlotDate <= end).ToList();
                }
            }
            return View(courses);
        }
           [HttpPost]
          public IActionResult ScheduleOfWeekTeacher(string date)
        {
            string[] dates = date.Trim().Split("/");
            DateTime start = new DateTime(2022, Convert.ToInt32(dates[1]), Convert.ToInt32(dates[0]));
            DateTime end = new DateTime(2022, Convert.ToInt32(dates[3]), Convert.ToInt32(dates[2]));
            ViewData["Start"] = start;
            ViewData["End"] = end;
            string TEACHER_ID = this.HttpContext.Session.GetString("TEACHER_ID");
            if (TEACHER_ID == null) return RedirectToAction(actionName: "Login", controllerName: "LoginRegister");
            List<CourseDetail> courses = null;
            using (var context = new SchoolDatabaseContext())
            {
                string teacherName = context.Teachers.FirstOrDefault(c => c.TeacherId == TEACHER_ID).FirstName + " " + context.Teachers.FirstOrDefault(c => c.TeacherId == TEACHER_ID).LastNamne;
                @ViewData["TeacherName"] = teacherName;

                courses = context.CourseDetails.Where(c => c.TeacherId == TEACHER_ID).ToList();
                foreach (CourseDetail c in courses)
                {
                    c.SlotInformations = context.SlotInformations.Where(s => s.SlotId == c.SlotId && s.SlotDate >= start && s.SlotDate <= end).ToList();
                }
                ViewBag.selectedDate = start;
            }
            return View(courses);
        }

        public IActionResult Attendance(string slotId,int SlotTimeName,DateTime date) {

            List<SlotAttendance> SlotAttendances = null;
            using (var context = new SchoolDatabaseContext())
            {
                string studentGroupID = context.CourseDetails.FirstOrDefault(c => c.SlotId == slotId).StudentGroupId;
                ViewData["SlotID"] = slotId;
                ViewData["StudentGroup"] = studentGroupID;
                if (studentGroupID != null)
                {
                    SlotAttendances = context.SlotAttendances.Where(s => s.SlotId == slotId && s.SlotTimeName == SlotTimeName && s.SlotDate== date && s.SlotDate.Month == 5).ToList();
                   foreach(SlotAttendance s in SlotAttendances)
                    {
                        s.Student = context.StudentGroupDetails.FirstOrDefault(student => student.StudentId.Equals(s.StudentId));
                        s.Student.Student = context.Students.FirstOrDefault(student => student.Rollnumber.Equals(s.StudentId));
                        
                    }
                }
                
            }
                return View(SlotAttendances);
        }
    
        public IActionResult SubmitAttendance(string[] slotAttendances,string SlotID ,DateTime Date , int SlotTimeName)
        {
            using (var context = new SchoolDatabaseContext())
            {
                SlotInformation si = context.SlotInformations.FirstOrDefault(s => s.SlotId == SlotID && s.SlotDate.Equals(Date) && s.SlotTimeName == SlotTimeName);
                si.SlotStatus = true;
                si.SlotNote = "Finish";
                List<SlotAttendance> attendances = context.SlotAttendances.Where(sa => sa.SlotId == SlotID && sa.SlotDate.Equals(Date) && sa.SlotTimeName == SlotTimeName).ToList();
                foreach(SlotAttendance slotAttendance in attendances)
                {
                    if (slotAttendances.Contains(slotAttendance.StudentId))
                    {
                        slotAttendance.Attendance = true;
                    }
                    else
                    {
                        slotAttendance.Attendance = false;
                    }
                }
                context.SaveChanges();
            }
                return RedirectToAction("ScheduleOfWeekTeacher");
        }

    }
}       

using Assigment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assigment.Controllers
{
    public class LoginRegisterController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            this.HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public IActionResult Login(string username,string password,int Role)
        {
            using (var context = new SchoolDatabaseContext())
            {
                if(context.Accounts.FirstOrDefault(x => x.Username == username && x.Password == password && x.Roleid == Role) != null)
                {
                    Account account = context.Accounts.FirstOrDefault(x => x.Username == username && x.Password == password && x.Roleid == Role);
                    switch (Role)
                    {
                        case 1:
                            Student Student = context.Students.FirstOrDefault(x => x.Rollnumber == account.Id);
                            if (Student != null) {
                                this.HttpContext.Session.SetInt32("ROLE", 1);
                                this.HttpContext.Session.SetString("STUDENT_ID", Student.Rollnumber);
                                this.HttpContext.Session.SetString("STUDENT_NAME", Student.FirstName + " " + Student.LastName);
                                return RedirectToAction(actionName: "ScheduleOfWeek", controllerName: "Timetable");
                                
                            }
                            ViewData["Error"] = "Login failed";
                            return RedirectToAction("Login");

                            break;
                        case 2:
                            Teacher teacher = context.Teachers.FirstOrDefault(t => t.TeacherId == account.Id);
                            if (teacher != null)
                            {
                                HttpContext.Session.SetInt32("ROLE", 2);
                                HttpContext.Session.SetString("TEACHER_ID", teacher.TeacherId);
                                HttpContext.Session.SetString("TEACHER_NAME", teacher.FirstName + " " + teacher.LastNamne);
                                return RedirectToAction(actionName: "ScheduleOfWeekTeacher", controllerName: "Timetable");
                               

                            }
                            ViewBag.error = "Login failed";
                            return RedirectToAction("Login");
                            break;
                        case 3:
                            break;
                        default:
                              
                            break;
                    }
                    
                }
            }
                return RedirectToAction("login");
        }

    }
}

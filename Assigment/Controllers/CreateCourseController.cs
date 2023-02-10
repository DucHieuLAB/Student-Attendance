using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assigment.Controllers
{
    public class CreateCourseController : Controller
    {
        public IActionResult Course()
        {
            return View();
        }
        public IActionResult CreateCourse()
        {
            return View();
        }
    }
}

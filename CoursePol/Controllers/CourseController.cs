using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoursePol.Controllers
{
    public class CourseController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(int? courseID)
        {
            if (courseID == null)
            {
                return NotFound();
            }
            return View();
        }
        public IActionResult Course(int? courseID)
        {
            if (courseID == null)
            {
                return NotFound();
            }
            return View();
        }
    }
}

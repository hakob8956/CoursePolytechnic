using CoursePol.Models;
using CoursePol.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;



namespace CoursePol.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public const int ImageMinimumBytes = 512;
        private readonly UserManager<User> _userManager;
        private readonly ICourseExercise _courseExercise;
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public AdminController(UserManager<User> userManager,ICourseExercise courseExercise)
        {
            _userManager = userManager;
            _courseExercise = courseExercise;

        }


        public ViewResult Index()
        {
            return View(_courseExercise.CourseExercises);
        }
        
    }
}
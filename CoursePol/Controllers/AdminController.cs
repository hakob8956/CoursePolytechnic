using CoursePol.Models;
using CoursePol.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace CoursePol.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public const int ImageMinimumBytes = 512;
        private readonly UserManager<User> _userManager;
        private readonly ICourseExercise _courseExercise;
        private readonly IFolower _folower;
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public AdminController(UserManager<User> userManager, ICourseExercise courseExercise, IFolower folower)
        {
            _userManager = userManager;
            _courseExercise = courseExercise;
            _folower = folower;

        }


        public ViewResult Index()
        {
            return View(_courseExercise.CourseExercises);
        }
        public IActionResult AjaxSearch(string search, int categoryID, string categoryName)
        {
            string myseach = search.ToLower().Replace(" ", "");
            IEnumerable<User> SelectedUsers = null;
            switch (categoryID)
            {
                case 2:
                    SelectedUsers = _userManager.Users.Where(i => i.Name.Contains(myseach) || i.Surname.Contains(myseach));
                    break;
                case 3:
                    SelectedUsers = _userManager.Users.Where(i => i.Institute == categoryName && (i.Name.Contains(myseach) || i.Surname.Contains(myseach)));
                    break;
                case 4:
                    SelectedUsers = _userManager.Users.Where(i => i.Department == categoryName && (i.Name.Contains(myseach) || i.Surname.Contains(myseach)));
                    break;
                case 5:
                    SelectedUsers = _userManager.Users.Where(i => i.Faculty == categoryName && (i.Name.Contains(myseach) || i.Surname.Contains(myseach)));
                    break;
                case 6:
                    SelectedUsers = _userManager.Users.Where(i => i.Group == categoryName && (i.Name.Contains(myseach) || i.Surname.Contains(myseach)));
                    break;
                default:
                    break;
            }

            UserDetailViewModel model = new UserDetailViewModel()
            {
                Users = SelectedUsers,
                CategoryID = 6

            };

            return PartialView("ShowUsers", model);
        }
        public IActionResult Home(int categoryID = 2, string categoryName = null, int AjaxRequest = 0)
        {

            //TODO FIX SEARCH TREEE!
            IEnumerable<string> currentFolders = null;
            IEnumerable<User> SelectedUsers = null;
            categoryName = categoryName?.TrimStart();
            switch (categoryID)
            {
                case 2:
                    currentFolders = _userManager.Users.Select(i => i.Institute).Distinct();
                    break;
                case 3:
                    currentFolders = _userManager.Users.Where(i => i.Institute == categoryName).Select(d => d.Department).Distinct();
                    break;
                case 4:
                    currentFolders = _userManager.Users.Where(i => i.Department == categoryName).Select(i => i.Faculty).Distinct();
                    break;
                case 5:
                    currentFolders = _userManager.Users.Where(i => i.Faculty == categoryName).Select(i => i.Group).Distinct();
                    break;
                case 6:
                    currentFolders = _userManager.Users.Where(i => i.Group == categoryName).Select(i => $"{i.Name} {i.Surname}").Distinct();
                    SelectedUsers = _userManager.Users.Where(i => i.Group == categoryName);
                    break;
                default:

                    break;
            }
            UserDetailViewModel model = new UserDetailViewModel
            {
                CurrentFolders = currentFolders,
                CategoryID = categoryID,
                Users = SelectedUsers,
                Folower = _folower.Folowers,
                CategoryName = categoryName
            };
            if (AjaxRequest == 1)
            {
                return PartialView("ShowUsers", model);
            }
            return View(model);

        }
        public IActionResult EditCourse()
        {
            return View(_courseExercise.CourseExercises);
        }

    }
}
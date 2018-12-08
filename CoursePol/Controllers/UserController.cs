using CoursePol.Models;
using CoursePol.Models.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
namespace CoursePol.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    MiddleName = model.MiddleName,
                    DateBirthday = model.DateBirthday,
                    DateRegister = model.DateRegister,
                    Institute = model.Institute,
                    Department = model.Department,
                    Faculty = model.Faculty,
                    Group = model.Group,
                    Email = model.Email,
                    UserName = model.Email

                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var resultRole = await _userManager.AddToRoleAsync(user, "user");
                    if (resultRole.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                MiddleName = user.MiddleName,
                DateBirthday = user.DateBirthday,
                DateRegister = user.DateRegister,
                Institute = user.Institute,
                Department = user.Department,
                Faculty = user.Faculty,
                Group = user.Group,
                Email = user.Email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.Surname = model.Surname;
                    user.MiddleName = model.MiddleName;
                    user.DateBirthday = model.DateBirthday;
                    user.Institute = model.Institute;
                    user.Department = model.Department;
                    user.Faculty = model.Faculty;
                    user.Group = model.Group;


                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    IdentityResult result =
                        await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User don't Found");
                }
            }
            return View(model);
        }
    }
}

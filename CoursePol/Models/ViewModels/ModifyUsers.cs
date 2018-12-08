using System;
using System.ComponentModel.DataAnnotations;
namespace CoursePol.Models.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        [MaxLength(22, ErrorMessage = "Max Length for Name 22 character")]
        [MinLength(3, ErrorMessage = "Min Length for Name 3 character")]
        public string Name { get; set; }
        [Required]
        [MaxLength(42, ErrorMessage = "Max Length for SurName 42 character")]
        [MinLength(3, ErrorMessage = "Min Length for SurName 3 character")]
        public string Surname { get; set; }
        [Required]
        [MaxLength(22, ErrorMessage = "Max Length for MiddleName 22 character")]
        [MinLength(3, ErrorMessage = "Min Length for MiddleName 3 character")]
        public string MiddleName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/2/1910", "3/4/2002",
        ErrorMessage = "Value for Date Birthday must be between 1/2/1910 and 3/4/2002")]
        public string DateBirthday { get; set; }
        public string DateRegister { get; set; }
        [Required(ErrorMessage = "The faculty name  is required")]
        public string Institute { get; set; }
        [Required(ErrorMessage = "The faculty name  is Department")]
        public string Department { get; set; }
        [Required(ErrorMessage = "The faculty name  is required")]
        public string Faculty { get; set; }
        [Required(ErrorMessage = "The group name   is required")]
        public string Group { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [StringLength(18, ErrorMessage = "The Password must be at least 8 characters long.", MinimumLength = 3)]
        //[DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        //[DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordConfirm { get; set; }
    }
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(22, ErrorMessage = "Max Length for Name 22 character")]
        [MinLength(3, ErrorMessage = "Min Length for Name 3 character")]
        public string Name { get; set; }
        [Required]
        [MaxLength(42, ErrorMessage = "Max Length for SurName 42 character")]
        [MinLength(3, ErrorMessage = "Min Length for SurName 3 character")]
        public string Surname { get; set; }
        [Required]
        [MaxLength(22, ErrorMessage = "Max Length for MiddleName 22 character")]
        [MinLength(3, ErrorMessage = "Min Length for MiddleName 3 character")]
        public string MiddleName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/2/1910", "3/4/2002",
        ErrorMessage = "Value for Date Birthday must be between 1/2/1910 and 3/4/2002")]
        public string DateBirthday { get; set; }
        public string DateRegister { get; set; }
        [Required(ErrorMessage = "The faculty name  is required")]
        public string Institute { get; set; }
        [Required(ErrorMessage = "The faculty name  is Department")]
        public string Department { get; set; }
        [Required(ErrorMessage = "The faculty name  is required")]
        public string Faculty { get; set; }
        [Required(ErrorMessage = "The group name   is required")]
        public string Group { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [StringLength(18, ErrorMessage = "The Password must be at least 8 characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(18, ErrorMessage = "The Password must be at least 8 characters long.", MinimumLength = 3)]
        public string NewPassword { get; set; }

    }
}

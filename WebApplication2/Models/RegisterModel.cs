using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "NameRequired")]
        public string Name { get; set; }

        [Required(ErrorMessage = "EmailRequired")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "EmailRegularExpression")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]
        [StringLength(20, ErrorMessage = "PasswordLength", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$", ErrorMessage = "PasswordErrorMessage")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "ConfirmPasswordError")]
        public string ConfirmPassword { get; set; }
    }
}

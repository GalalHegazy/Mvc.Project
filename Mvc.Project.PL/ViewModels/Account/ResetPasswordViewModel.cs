using System.ComponentModel.DataAnnotations;

namespace Mvc.Project.PL.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = " New Password Is Required")]
        [MinLength(5,ErrorMessage ="Minimum Password Length is 5")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare(nameof(NewPassword), ErrorMessage = "Password Does't Match ")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}

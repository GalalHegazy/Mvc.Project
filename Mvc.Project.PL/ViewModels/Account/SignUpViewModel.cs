using System.ComponentModel.DataAnnotations;

namespace Mvc.Project.PL.ViewModels.Account
{
    public class SignUpViewModel 
    {
        [Required(ErrorMessage = "User Name Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage ="Invaild Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Password Does't Match ")]
        public string ConfirmPassword { get; set; }
    }
}

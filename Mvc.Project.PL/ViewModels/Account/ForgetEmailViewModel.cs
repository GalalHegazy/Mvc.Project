using System.ComponentModel.DataAnnotations;

namespace Mvc.Project.PL.ViewModels.Account
{
    public class ForgetEmailViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invaild Email")]
        public string Email { get; set; }
    }
}

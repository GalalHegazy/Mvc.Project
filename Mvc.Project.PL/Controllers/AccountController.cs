using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mvc.Project.PL.Servies.EmailSender;
using Mvc.Project.PL.ViewModels.Account;
using System.Threading.Tasks;

namespace Mvc.Project.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager
                               , SignInManager<IdentityUser> signInManager
                               , RoleManager<IdentityRole> roleManager
                               ,IEmailSender emailSender
                               ,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _configuration = configuration;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel SignUpVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(SignUpVM.UserName);

                if (user is null)
                {
                    user = new IdentityUser()
                    {
                        UserName = SignUpVM.UserName,
                        Email = SignUpVM.Email,
                    };

                    var result = await _userManager.CreateAsync(user, SignUpVM.Password);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(LogIn));

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                ModelState.AddModelError(string.Empty, "User Name Is Already Use For Another Account");
            }
            return View(SignUpVM);
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel logInVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(logInVM.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, logInVM.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, logInVM.Password, logInVM.RememberMe, false);

                        if (result.IsLockedOut)
                            ModelState.AddModelError(string.Empty, "Your Account Is Locked!!");

                        if (result.IsNotAllowed)
                            ModelState.AddModelError(string.Empty, "Your Account Is Not Confirmed Yet..");

                        if (result.Succeeded)
                            //TempData["UserName"]=user.UserName;
                            //TempData.Keep();
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }

            }
            ModelState.AddModelError(string.Empty, "Invalid Email!");

            return View(logInVM);
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }
        public IActionResult ForgetPassword()
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> SendResetEmailPassword(ForgetEmailViewModel forgetEmailVM)
        {
            if (ModelState.IsValid) 
            {
                var user = await _userManager.FindByEmailAsync(forgetEmailVM.Email);
                if (user is not null)
                {

                    var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    //Create Url for Action Reset Password  exp(https://localhost:5505/Account/ResetPassword?email=galal@gamil.com$token=6595161498df84ff8f)
                    var resetPasswordUrl = Url.Action("ResetPassword", "Account", new { email = user.Email,token = resetPasswordToken}, "https"); 

                    await _emailSender.SendAsync(
                        from: _configuration["EmailSetting:SenderEmail"],
                        recipients: forgetEmailVM.Email,
                        subject: "Reset Your Password",
                        body: resetPasswordUrl);

                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "There Is No Account With This Email");
            }
            return View(forgetEmailVM);
        }

        public IActionResult CheckYourInbox()
        {
            return View();  
        }
        [HttpGet]
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["Email"]=email;
            TempData["token"]=token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordVM)
        {
            if (ModelState.IsValid)
            {
                var Email = TempData["Email"] as string;
                var Token = TempData["token"] as string;

                var User = await _userManager.FindByEmailAsync(Email);

                if(User is not null)
                {
                     await _userManager.ResetPasswordAsync(User, Token, resetPasswordVM.NewPassword);
                    return RedirectToAction(nameof(LogIn));
                }
                ModelState.AddModelError(string.Empty, "Url Is Not Found !!");
            }
            return View(resetPasswordVM);
        }
    }
}

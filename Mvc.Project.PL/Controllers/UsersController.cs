using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc.Project.PL.Extensions;
using Mvc.Project.PL.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Project.PL.Controllers
{
    [Authorize(Roles = AppPermissions.Admin)]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<IdentityUser> userManager,
                               IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchInputUser)
        {
            if (string.IsNullOrEmpty(SearchInputUser))
            {
                var Users = await _userManager.Users.Select(
                 U => new UserViewModel()
                 {
                     Id = U.Id,
                     UserName = U.UserName,
                     Email = U.Email,
                     PhoneNumber = U.PhoneNumber,
                     Roles = _userManager.GetRolesAsync(U).Result
                 }).ToListAsync();
                return View(Users); 
            }
            else
            {
                var Users = await _userManager.FindByEmailAsync(SearchInputUser);
                var MappedUser = new UserViewModel()
                {
                    Id = Users.Id,
                    UserName = Users.UserName,
                    Email = Users.Email,
                    PhoneNumber = Users.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(Users).Result
                };

                return PartialView("PartialViews/UserTablePartial", new List<UserViewModel> { MappedUser });
            }
        }
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);

            if(user is null)
                return NotFound();

            var MappedUser= _mapper.Map<UserViewModel>(user);

            return View(viewName, MappedUser);

        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, nameof(Update));
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel userVM,[FromRoute] string id)
        {
            if(id != userVM.Id )
                return BadRequest();

            if(ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.UserName = userVM.UserName;
                    user.PhoneNumber = userVM.PhoneNumber;  
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex )
                {
                    ModelState.AddModelError(string.Empty, ex.Message);    
                }
            }
           return View(userVM);
        }
        public Task<IActionResult> Delete(string Id)
        {
            return Details(Id, nameof(Delete)); 
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string email)
        {
            try
            {
                var User = await _userManager.FindByEmailAsync(email);
                await _userManager.DeleteAsync(User);
                return RedirectToAction(nameof(Index)); 

            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

    }
}

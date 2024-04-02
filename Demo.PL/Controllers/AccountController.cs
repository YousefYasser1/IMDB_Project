
using Demo.DAL.Entites;
using Demo.PL.Const;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRegister(RegisterViewModel newModel)
        {
            if(ModelState.IsValid)
            {

                var user = new ApplicationUser();
                var imageString = DocumentSetting.UploadFile(newModel.ImageProfile, "UsersImages");
                user.UserName = newModel.Email.Split('@')[0];
                user.Email = newModel.Email;
                user.Address = newModel.Address;
                user.Age = newModel.Age;
                user.ImageName = imageString;

                var result = await _userManager.CreateAsync(user, newModel.Password);
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleNames.User.ToString());
                    await _signInManager.SignInAsync(user, false);
                    TempData[NotificationMode.Successflly.ToString()] = "Register Done Successfully";
                    return RedirectToAction("Index", "HomeMovie");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            
            return View("Register", newModel);
        }


        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveLogin(LoginViewModel newModel)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(newModel.Email);
                if(user != null)
                {
                    bool flag = await _userManager.CheckPasswordAsync(user, newModel.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, newModel.Password, newModel.RememberMe, false);
                        if (result.Succeeded)
                        {
                            TempData[NotificationMode.UpdateData.ToString()] = "Login Done Successfully";
                            return RedirectToAction("Index", "HomeMovie");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Password is Wrong");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email is not Valid");
                }
            }

            return View("Login" ,newModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


        public async Task<IActionResult> EditProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                EditProfileViewModel model = new EditProfileViewModel()
                {
                    Id = user.Id,
                    Address = user.Address,
                    Age = user.Age,
                    UserName = user.UserName
                };
                return View(model);
            }
            return RedirectToAction(nameof(Login));
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditProfile(EditProfileViewModel newModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if(user != null)
                {
                    user.UserName = newModel.UserName;
                    user.Address = newModel.Address; 
                    user.Age = newModel.Age;

                    await _userManager.UpdateAsync(user);

                    await Logout();
                    return RedirectToAction(nameof(ViewOfUdpatedUserProfile));
                }
                else
                {
                    ModelState.AddModelError("", "User Not Found");
                }
            }

            return View(newModel);
        }

        public async Task<IActionResult> ViewOfUdpatedUserProfile()
        {
            return View();
        }
    }
}

using Demo.DAL.Entites;
using Demo.PL.Const;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountAdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountAdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //public SignInManager<ApplicationUser> SignInManager { get; }

        public async Task<IActionResult> AddAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SaveAdmin(AdminRegsiterViewModel newModel)
        {
            if(ModelState.IsValid)
            {
                var imageString = DocumentSetting.UploadFile(newModel.ImageFile, "AdminsImages");
                var user = new ApplicationUser();
                user.UserName = newModel.Email.Split('@')[0];
                user.Email = newModel.Email;
                user.ImageName = imageString;

                var result = await _userManager.CreateAsync(user, newModel.Password);
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleNames.Admin.ToString());
                    await _signInManager.SignInAsync(user, false);
                    TempData[NotificationMode.Successflly.ToString()] = "Genre Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View("AddAdmin", newModel);
        }

        public async Task<IActionResult> Index()
        {
            var admins = await _userManager.GetUsersInRoleAsync(RoleNames.Admin.ToString());
            return View(admins);
        }
    }
}

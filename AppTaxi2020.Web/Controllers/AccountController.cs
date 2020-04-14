using AppTaxi2020.Web.Data.Entities;
using AppTaxi2020.Web.Helpers;
using AppTaxi2020.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AppTaxi2020.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly ICombosHelper _combosHelper;

        public AccountController(IUserHelper userHelper, IImageHelper imageHelper, ICombosHelper combosHelper)
        {
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _combosHelper = combosHelper;
        }

        public async Task<IActionResult> ChangeUser()
        {
            Data.Entities.UserEntity user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
            EditUserViewModel model = new EditUserViewModel
            {
                Address = user.Address,
                Document = user.Document,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PicturePath = user.PicturePath,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = model.PicturePath;

                if (model.PictureFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.PictureFile, "Users");
                }

                UserEntity user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

                user.Document = model.Document;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                user.PicturePath = path;

                await _userHelper.UpdateUserAsync(user);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(loginViewModel);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Failed to login.");
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            AddUserViewModel model = new AddUserViewModel
            {
                UserTypes = _combosHelper.GetComboRoles(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;
                if (model.PictureFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.PictureFile, "Users");
                }

                Data.Entities.UserEntity user = await _userHelper.AddUserAsync(model, path);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "This email is already used.");
                    model.UserTypes = _combosHelper.GetComboRoles();
                    return View(model);
                }

                //Aqui logeo al uasuario nuevo:
                LoginViewModel loginViewModel = new LoginViewModel
                {
                    Password = model.Password,
                    RememberMe = false,
                    Username = model.Username,
                };
                Microsoft.AspNetCore.Identity.SignInResult logIn = await _userHelper.LoginAsync(loginViewModel);
                if (logIn.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            model.UserTypes = _combosHelper.GetComboRoles();
            return View(model);
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }
    }
}


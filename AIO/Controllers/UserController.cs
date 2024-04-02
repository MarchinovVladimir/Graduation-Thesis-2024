using AIO.Data.Models;
using AIO.Web.ViewModels.User;
using Ganss.Xss;
using Griesoft.AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static AIOCommon.ErrorMessageConstants.ApplicationUser;
using static AIOCommon.GeneralAppConstants;

namespace AIO.Controllers
{
	/// <summary>
	/// User controller for handling user registration and login.
	/// </summary>
	public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMemoryCache memoryCache;

        public UserController(SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager,
                              IMemoryCache memoryCache)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;

            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// Register get method for displaying the registration form.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Register post method for handling the registration form submission.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRecaptcha(Action = nameof(Register), ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            HtmlSanitizer sanitizer = new HtmlSanitizer();
            model.Email = sanitizer.Sanitize(model.Email);
            model.Password = sanitizer.Sanitize(model.Password);
            model.ConfirmPassword = sanitizer.Sanitize(model.ConfirmPassword);
            model.FirstName = sanitizer.Sanitize(model.FirstName);
            model.LastName = sanitizer.Sanitize(model.LastName);


            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                await userManager.SetUserNameAsync(user, model.Email);
                await userManager.SetEmailAsync(user, model.Email);

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View(model);
                }

                await signInManager.SignInAsync(user, isPersistent: false);

                memoryCache.Remove(UsersCacheKey);  

                return RedirectToAction("Index", "Home");

            }
            return View(model);
        }

        /// <summary>
        /// Login get method for displaying the login form.
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            LoginFormModel model = new LoginFormModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        /// <summary>
        /// Login post method for handling the login form submission.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRecaptcha(Action = nameof(Login), ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            HtmlSanitizer sanitizer = new HtmlSanitizer();

            model.Email = sanitizer.Sanitize(model.Email);
            model.Password = sanitizer.Sanitize(model.Password);

            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, InvalidLoginAttemptErrorMessage);
                    return View(model);
                }

                Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, InvalidLoginAttemptErrorMessage);
            }

            return View(model);
        }
    }
}

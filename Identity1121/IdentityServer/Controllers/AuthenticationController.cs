using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<User> _userMgr;
        private readonly SignInManager<User> _signInMgr;

        public AuthenticationController(/*TestUserStore users = null*/UserManager<User> userMgr, SignInManager<User> signInMgr)
        {
            //_users = users ?? new TestUserStore(TestUsers.Users);
            _signInMgr = signInMgr;
            _userMgr = userMgr;
        }

        [Route("login", Name = "UrlLogin")]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            //AD authenticated
            /*if (this.User.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl);
            }*/


            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }



        [Route("login")]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInMgr.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return Redirect(model.ReturnUrl);
                }

            }
            ModelState.AddModelError("Email", "Login / mot de passe invalide");
            return View();

        }

        [Route("logout")]
        [HttpGet]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            if (User?.Identity.IsAuthenticated == true)
            {
                await _signInMgr.SignOutAsync();
            }
            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);
            return Redirect("https://localhost:5801/signout-oidc");
        }

        [Route("exter", Name = "exter")]
        [HttpGet]
        public IActionResult Exter(string returnUrl)
        {
            /*var callback = Url.Action("SigninExter");
            var props = new AuthenticationProperties
            {
                RedirectUri = "https://localhost:5001/Authentication/SigninExter",
                Items =
                {
                    {"scheme" ,  IdentityServerConstants.ExternalCookieAuthenticationScheme},
                    {"returnUrl", returnUrl }
                }
            };

            return Challenge(props, "microsoftaccount");
            */
            return null;
        }

        //[Route("signin-microsoft", Name = "SigninExter")]
        public async Task<IActionResult> SigninExter(string returnUrl)
        {
            //var result = await HttpContext.AuthenticateAsync("microsoftaccount");

            return BadRequest();
        }
    }
}

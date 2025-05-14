using CoddingAssessmentProject.Repositories.ViewModels;
using CoddingAssessmentProject.Services.Intefaces;
using CoddingAssessmentProject.Services.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoddingAssessmentProject.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IJwtService _jwtService;

        private readonly IServiceProvider _serviceProvider;

        #region  Constructor
        public AuthenticationController(IAuthenticationService authenticationService, IJwtService jwtService, IServiceProvider serviceProvider)
        {


            _authenticationService = authenticationService;
            _jwtService = jwtService;
            _serviceProvider = serviceProvider;
        }
        #endregion

        #region UserLogin GET
        public IActionResult UserLogin()
        {
            return View();
        }
        #endregion
        #region UserLogin POST
        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                UserLoginViewModel? user = await _authenticationService.AuthenticateUserUsingEmailPassword(
                    model.UserEmail!.ToLower(),
                    model.UserPassword!
                );

                if (user == null)
                {
                    bool userExists = await _authenticationService.CheckIfUserExists(model.UserEmail.ToLower());
                    if (userExists)
                    {
                        ModelState.AddModelError(
                            "UserPassword",
                            "Invalid password. Try again or reset your password."
                        );
                    }
                    return View(model);
                }
                if (user.UserEmail == null)
                {
                    ModelState.AddModelError(
                        "UserEmail",
                        "Invalid email. Try again or reset your password."
                    );
                    return View(model);
                }
                string? token = await _jwtService.GenerateJwtToken(user.UserEmail, model.RememberMe);



                CookieUtils.SaveJWTToken(Response, token);

                if (model.RememberMe)
                {
                    CookieUtils.SaveUserData(Response, user);
                }
                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while processing your request. Please try again.";
                return View();
            }
        }
        #endregion

        #region LogOut 
        public IActionResult Logout()
        {
            try
            {
                CookieUtils.ClearCookies(HttpContext);
                SessionUtils.ClearSession(HttpContext);
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while processing your request. Please try again.";
                return RedirectToAction("Login", "Auth");
            }
        }
        #endregion
    }
}

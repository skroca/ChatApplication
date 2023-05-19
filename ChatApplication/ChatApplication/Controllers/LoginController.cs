using ChatApplication.Models;
using Data.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatApplication.Controllers
{
    public class LoginController : Controller
    {

        [BindProperty]
        public LoginViewModel Credentials { get; set; }


        public readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            if (!ModelState.IsValid) return View("Index");

            if(_loginService.Login(Credentials.UserName ?? "", Credentials.Password ?? ""))
            {
                var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, Credentials.UserName),
                new Claim(ClaimTypes.Email, Credentials.UserName)};

                var identity = new ClaimsIdentity(claims, "Cookies");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("Cookies", claimsPrincipal);
                return RedirectToAction("Index", "Chat");
            }
            return View("Index");

        }
    }
}

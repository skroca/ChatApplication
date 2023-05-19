using ChatApplication.Models;
using Data.DTOModels;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatApplication.Controllers
{
    public class UserController : Controller
    {
        public readonly IUserService _userService;
        private readonly ILogger _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [BindProperty]
        public UserDTO User { get; set; }
        public IActionResult AddUser()
        {
            try
            {
                var errorList = User.Validate();

                if (errorList.Count > 0)
                {
                    return View("Index",User);
                }
                _userService.AddUser(User);
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                User.ErrorMessage = ex.Message;
                return View("Index", User);
            }
        }
    }
}

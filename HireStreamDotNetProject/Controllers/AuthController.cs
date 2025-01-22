// created manually!
// this controller will contain routes for login, logout, register, edit user, delete user, forgot password, etc.

using Microsoft.AspNetCore.Mvc;

namespace HireStreamDotNetProject.Controllers
{
    public class AuthController : Controller
    {
        // GET: /Auth/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signup() {
            return View();
        }

        public IActionResult EditUser() {
            return View();
        }

        public IActionResult ForgotPassword() {
            return View();
        }

        public IActionResult Logout() {
            return View(); // this does not need a view, so use something like a redirect
        }
    }
}

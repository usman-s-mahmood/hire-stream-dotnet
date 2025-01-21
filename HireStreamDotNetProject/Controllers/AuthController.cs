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
    }
}

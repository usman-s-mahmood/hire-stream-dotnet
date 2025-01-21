// Created Manually!
// all admin related stuff like managing users, job details, etc.

using Microsoft.AspNetCore.Mvc;

namespace HireStreamDotNetProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: /Admin/
        public IActionResult Index()
        {
            return View();
        }
    }
}

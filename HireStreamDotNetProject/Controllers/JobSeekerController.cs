// Created Manually!
// dedicated for users who want to search for jobs. This will include job posts, search section, job application forms, etc.

using Microsoft.AspNetCore.Mvc;

namespace HireStreamDotNetProject.Controllers
{
    public class JobSeekerController : Controller
    {
        // GET: /JobSeeker/
        public IActionResult Index()
        {
            return View();
        }
    }
}

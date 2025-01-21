// Created manually!
// Dedicated to HRs and talent sourcers who will be posting jobs. This will contain routes like post job, application details, etc.

using Microsoft.AspNetCore.Mvc;

namespace HireStreamDotNetProject.Controllers
{
    public class TalentController : Controller
    {
        // GET: /Talent/
        public IActionResult Index()
        {
            return View();
        }
    }
}

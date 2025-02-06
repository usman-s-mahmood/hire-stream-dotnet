using Microsoft.AspNetCore.Mvc;

namespace HireStreamDotNetProject.Controllers;
public class ErrorController : Controller
{
    [Route("Error/404")]
    public IActionResult Error404()
    {
        return View("NotFound");
    }

    [Route("Error/500")]
    public IActionResult Error500()
    {
        return View("ServerError");
    }
}

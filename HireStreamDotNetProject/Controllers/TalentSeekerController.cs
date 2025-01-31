// Created manually!
// Dedicated to HRs and talent sourcers who will be posting jobs. This will contain routes like post job, application details, etc.

using Microsoft.AspNetCore.Mvc;
using HireStreamDotNetProject.Data;
using HireStreamDotNetProject.Utils;
using HireStreamDotNetProject.Models;

namespace HireStreamDotNetProject.Controllers
{
    public class TalentController : Controller
    {
        
        TokenService _tokenService;
        ApplicationDbContext _db;
        public TalentController(ApplicationDbContext db, TokenService tokenService) {
            _db = db;
            _tokenService = tokenService;
        }
        // GET: /Talent/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePost() {
            var auth_cookie = Request.Cookies["AuthCookie"];
            if (auth_cookie == null) {
                TempData["error"] = "Login To Continue!";
                return RedirectToAction("Login", "Auth");
            }
            string? email;
            string? username;
            User? user;

            try {
                var payload = _tokenService.DecryptToken(auth_cookie);
                var data = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(payload);

                email = data["email"];
                username = data["username"];

                user = _db.Users.FirstOrDefault(o => o.Email == email && o.Username == username);

                if (user == null) {
                    Response.Cookies.Delete("AuthCookie");
                    TempData["error"] = "Authentication Failed!";
                    return RedirectToAction("Login");
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}

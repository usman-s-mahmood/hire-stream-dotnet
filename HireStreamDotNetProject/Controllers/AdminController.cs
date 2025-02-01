// Created Manually!
// all admin related stuff like managing users, job details, etc.

using Microsoft.AspNetCore.Mvc;
using HireStreamDotNetProject.Models;
using HireStreamDotNetProject.Data;
using HireStreamDotNetProject.Utils;

namespace HireStreamDotNetProject.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext _db;
        TokenService _tokenService;
        public AdminController(ApplicationDbContext db, TokenService tk) {
            _db = db;
            _tokenService = tk;
        }
        // GET: /Admin/
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateCategory() {
            var auth_cookie = Request.Cookies["AuthCookie"];
            Console.WriteLine($"value of auth token: {auth_cookie}");
            if (auth_cookie == "") {
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
                    return RedirectToAction("Login", "Auth");
                }

                if (!user.IsAdmin && !user.IsStaff) {
                    TempData["error"] = "Invalid Request!";
                    return RedirectToAction(
                        "Dashboard",
                        "Auth"
                    );
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(JobCategory obj) {
            string? auth_cookie = Request.Cookies["AuthCookie"];
            Console.WriteLine($"value of auth token: {auth_cookie}");
            if (auth_cookie == "") {
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
                    return RedirectToAction("Login", "Auth");
                }

                if (!user.IsAdmin && !user.IsStaff) {
                    TempData["error"] = "Invalid Request!";
                    return RedirectToAction(
                        "Dashboard",
                        "Auth"
                    );
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");
            }
            System.Console.WriteLine($"Name Received: {obj.Name}");
            JobCategory? check = _db.JobCategories.FirstOrDefault(o => o.Name == obj.Name);
            if (check != null) {
                ModelState.AddModelError(
                    "Name",
                    "Provide A Unique Category Name!"
                );
                TempData["error"] = "Provide A Unique name for category!";
                return View(obj);
            }
            _db.JobCategories.Add(new JobCategory{
                Name = obj.Name.ToLower(),
                User = user
            });
            _db.SaveChanges();
            TempData["success"] = $"{obj.Name} is now added as a category!";
            return RedirectToAction(
                "Dashboard",
                "Auth"
            );
        }
    
    }
}

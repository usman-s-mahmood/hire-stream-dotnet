// Created manually!
// Dedicated to HRs and talent sourcers who will be posting jobs. This will contain routes like post job, application details, etc.

using Microsoft.AspNetCore.Mvc;
using HireStreamDotNetProject.Data;
using HireStreamDotNetProject.Utils;
using HireStreamDotNetProject.Models;
using DevOne.Security.Cryptography.BCrypt;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public IActionResult CreatePost() {
            var auth_cookie = Request.Cookies["AuthCookie"];
            Console.WriteLine($"value of auth token: {auth_cookie}");
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
                    return RedirectToAction("Login", "Auth");
                }

                if (user.UserRole != "recruiter") {
                    TempData["error"] = "You need a recruiter account to perform this operation";
                    return RedirectToAction("Dashboard", "Auth");
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");

            }
            var JCats = _db.JobCategories.OrderBy(o => o.Name).ToList();
            ViewBag.JCats = JCats;
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(JobPost obj) {
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
                    return RedirectToAction("Login", "Auth");
                }

                if (user.UserRole != "recruiter") {
                    TempData["error"] = "You need a recruiter account to perform this operation";
                    return RedirectToAction("Dashboard", "Auth");
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");
            }
            // Console.WriteLine($"Value received in category field: {obj.CategoryId}");
            JobCategory? cat = _db.JobCategories.Find(obj.CategoryId);

            if (cat == null) {
                ModelState.AddModelError(
                    "",
                    "Invalid Job Category!"
                );
                TempData["error"] = "Invalid Job Category!";
                return View(obj);
            }

            _db.JobPosts.Add(new JobPost{
                Title = obj.Title,
                Content = obj.Content,
                JobType = obj.JobType,
                User = user,
                Location = obj.Location,
                Salary = obj.Salary,
                Qualification = obj.Qualification,
                JobCategory = cat
            });
            _db.SaveChanges();
            TempData["success"] = "Your post is now published!";
            return RedirectToAction("Dashboard", "Auth");
        }

        [HttpGet]
        public IActionResult EditPost(int id) {
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
                    return RedirectToAction("Login", "Auth");
                }

                if (user.UserRole != "recruiter") {
                    TempData["error"] = "You need a recruiter account to perform this operation";
                    return RedirectToAction("Dashboard", "Auth");
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");
            }
            
            JobPost? obj = _db.JobPosts.Find(id);
            if (obj == null) {
                TempData["error"] = "Post Not Found!";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            if (obj.UserId != user.Id && !user.IsAdmin) {
                TempData["error"] = "Invalid Operation! Request Failed";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            var JCats = _db.JobCategories.OrderBy(o => o.Name).ToList();
            ViewBag.JCats = JCats;
            return View(obj);
        }

        [HttpPost]
        public IActionResult EditPost(JobPost edit) {
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
                    return RedirectToAction("Login", "Auth");
                }

                if (user.UserRole != "recruiter") {
                    TempData["error"] = "You need a recruiter account to perform this operation";
                    return RedirectToAction("Dashboard", "Auth");
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");
            }
            Console.WriteLine($"Object Id Received: {edit.Id}");
            JobPost? obj = _db.JobPosts.Find(edit.Id);
            if (obj == null) {
                TempData["error"] = "Post Not Found!";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            if (obj.UserId != user.Id && !user.IsAdmin) {
                TempData["error"] = "Invalid Operation! Request Failed";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            obj.Title = edit.Title;
            obj.Location = edit.Location;
            obj.JobType = edit.JobType;
            obj.CategoryId = edit.CategoryId;
            obj.Qualification = edit.Qualification;
            obj.Salary = edit.Salary;
            obj.Content = edit.Content;
            _db.JobPosts.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Your post is now edited!";
            return RedirectToAction(
                "Dashboard",
                "Auth"
            );
        }
    
        
        [HttpGet]
        public IActionResult HidePost(int id) {
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
                    return RedirectToAction("Login", "Auth");
                }

                if (user.UserRole != "recruiter") {
                    TempData["error"] = "You need a recruiter account to perform this operation";
                    return RedirectToAction("Dashboard", "Auth");
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");
            }
            JobPost? obj = _db.JobPosts.Find(id);
            if (obj == null) {
                TempData["error"] = "Post Not Found!";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            if (obj.UserId != user.Id && !user.IsAdmin) {
                TempData["error"] = "Invalid Operation! Request Failed";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            if (!obj.IsActive) {
                TempData["error"] = "Post Is Already Hidden!";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            
            return View(obj);
        }

        
        [HttpPost]
        public IActionResult HidePost(string password, int Id) {
            System.Console.WriteLine($"Post Id forwarded to function is: {Id}");
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
                    return RedirectToAction("Login", "Auth");
                }

                if (user.UserRole != "recruiter") {
                    TempData["error"] = "You need a recruiter account to perform this operation";
                    return RedirectToAction("Dashboard", "Auth");
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");
            }
            JobPost? obj = _db.JobPosts.Find(Id);
            if (obj == null) {
                TempData["error"] = "Post Not Found!";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            if (obj.UserId != user.Id && !user.IsAdmin) {
                TempData["error"] = "Invalid Operation! Request Failed";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            if (!obj.IsActive) {
                TempData["error"] = "Post Is Already Hidden!";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            var password_check = BCryptHelper.CheckPassword(
                password,
                user.Password
            );
            if (!password_check) {
                ModelState.AddModelError(
                    "",
                    "Invalid Password, Try Again!"
                );
                TempData["error"] = "Incorrect Password! Try Again";
                return View(obj);
            }
            obj.IsActive = false;
            _db.JobPosts.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Your Post is now hidden";
            return RedirectToAction(
                "Dashboard",
                "Auth"
            );
        }
    
        [HttpGet]
        public IActionResult DeletePost(int id) {
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
                    return RedirectToAction("Login", "Auth");
                }

                if (user.UserRole != "recruiter") {
                    TempData["error"] = "You need a recruiter account to perform this operation";
                    return RedirectToAction("Dashboard", "Auth");
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");
            }
            JobPost? obj = _db.JobPosts.Find(id);
            if (obj == null) {
                TempData["error"] = "Post Not Found!";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            if (obj.UserId != user.Id && !user.IsAdmin) {
                TempData["error"] = "Invalid Operation! Request Failed";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            
            return View(obj);
        }

        
        [HttpPost]
        public IActionResult DeletePost(string password, int Id) {
            System.Console.WriteLine($"Post Id forwarded to function is: {Id}");
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
                    return RedirectToAction("Login", "Auth");
                }

                if (user.UserRole != "recruiter") {
                    TempData["error"] = "You need a recruiter account to perform this operation";
                    return RedirectToAction("Dashboard", "Auth");
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");
            }
            JobPost? obj = _db.JobPosts.Find(Id);
            if (obj == null) {
                TempData["error"] = "Post Not Found!";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            if (obj.UserId != user.Id && !user.IsAdmin) {
                TempData["error"] = "Invalid Operation! Request Failed";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            var password_check = BCryptHelper.CheckPassword(
                password,
                user.Password
            );
            if (!password_check) {
                ModelState.AddModelError(
                    "",
                    "Invalid Password, Try Again!"
                );
                TempData["error"] = "Incorrect Password! Try Again";
                return View(obj);
            }
            _db.JobPosts.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Your Post is now delete";
            return RedirectToAction(
                "Dashboard",
                "Auth"
            );
        }
    
    }
}

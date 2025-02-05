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
        EmailService _emailService;
        public TalentController(ApplicationDbContext db, TokenService tokenService, EmailService emailService) {
            _db = db;
            _tokenService = tokenService;
            _emailService = emailService;
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
        public IActionResult UnhidePost(int id) {
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
            if (obj.IsActive) {
                TempData["error"] = "Post Is Already Active!";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            
            return View(obj);
        }

        
        [HttpPost]
        public IActionResult UnhidePost(string password, int Id) {
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
            if (obj.IsActive) {
                TempData["error"] = "Post Is Already Active!";
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
            obj.IsActive = true;
            _db.JobPosts.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Your Post is now Active";
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
        public IActionResult Applications(int post_id, int page = 1) {
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

            var check_post = _db.JobPosts
                .Include(j => j.User)
                .FirstOrDefault(o => o.Id == post_id);
            System.Console.WriteLine($"Values in check post: {check_post.Title} | Null check {check_post == null}");
            if (check_post.UserId != user.Id && !user.IsAdmin && !user.IsStaff) {
                TempData["error"] = "Invalid Operation! Request Failed";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }

            if (check_post == null) {
                TempData["error"] = "No Record Found!!!";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }

            var jobPosts = _db.JobApplications
                .Include(j => j.JobPost)
                .Include(u => u.User)
                .OrderByDescending(o => o.Id)
                .Where(ob => ob.JobPostId == post_id);          
            
            int pageSize = 2;
            int totalJobs = jobPosts.Count();
            System.Console.WriteLine($"Total Records: {totalJobs}");
            var paginatedJobs = jobPosts.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Cards = paginatedJobs;
            ViewBag.CardCount = totalJobs;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalJobs / (double)pageSize);
            ViewBag.post_id = post_id;
            return View();
        }

        public IActionResult AppDetails(int app_id) {
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

            var app = _db.JobApplications
                .Include(o => o.JobPost)
                .Include(o => o.JobPost.User)
                .Include(o => o.User)
                .FirstOrDefault(o => o.Id == app_id);

            if (app.JobPost.UserId != user.Id && !user.IsAdmin && !user.IsStaff) {
                TempData["error"] = "Invalid Request! Process Failed";
                return Redirect("/Auth/Dashboard");
            }

            if (app == null) {
                TempData["error"] = "No record found!";
                return Redirect($"/Talent/Applications?post_id={app.JobPost.Id}");
            }
            ViewBag.Application = app;
            return View();
        }
    
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int app_id, string status) {
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
            var app = _db.JobApplications
                .Include(o => o.JobPost)
                .Include(o => o.User)
                .FirstOrDefault(o => o.Id == app_id);

            if (app == null) {
                TempData["error"] = "No Record found!";
                return Redirect("/Auth/Dashboard");
            }

            if (app.JobPost.UserId != user.Id && !user.IsAdmin && !user.IsStaff) {
                TempData["error"] = "Invalid Request! Process Failed";
                return Redirect("/Auth/Dashboard");
            }

            if (app.Status == status) {
                TempData["error"] = $"{status} is already the status of this application!";
                return Redirect($"/Talent/AppDetails?app_id={app_id}");
            }

            await _emailService.SendEmailAsync(
                app.User.Email,
                "Application Status At HireStream",
                $"Dear {app.User.FirstName}, \nThe Status of your application for job post: {app.JobPost.Title} has been updated from {app.Status} to {status}. \nBest Regards, \nIT Team (Hire Stream)"
            );

            app.Status = status;
            _db.JobApplications.Update(app);
            _db.SaveChanges();
            TempData["success"] = "Status for application is now updated!";
            return Redirect("/Auth/Dashboard");
        }
    }
}

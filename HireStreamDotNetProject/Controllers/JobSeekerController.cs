// Created Manually!
// dedicated for users who want to search for jobs. This will include job posts, search section, job application forms, etc.

using Microsoft.AspNetCore.Mvc;
using HireStreamDotNetProject.Data;
using HireStreamDotNetProject.Utils;
using HireStreamDotNetProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using DevOne.Security.Cryptography.BCrypt;

namespace HireStreamDotNetProject.Controllers
{
    public class JobSeekerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly TokenService _tokenService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public JobSeekerController(ApplicationDbContext db, TokenService tokenService, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor) {
            _db = db;
            _tokenService = tokenService;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET: /JobSeeker/
        public IActionResult Index()
        {
            return RedirectToAction("FindJobs");
        }

        [HttpGet]
        public IActionResult FindJobs(int page = 1) {
            int pageSize = 3;
            var jobPosts = _db.JobPosts
                .Include(j => j.JobCategory)
                .Where(o => o.IsActive == true)
                .OrderBy(o => o.Id)
                .Reverse();
            int totalJobs = jobPosts.Count();
            var paginatedJobs = jobPosts.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            if (page > (int) Math.Ceiling(totalJobs/(double)pageSize)) {
                TempData["error"] = "Record Not Found!";
                return RedirectToAction("FindJobs");
            }
            ViewBag.Cards = paginatedJobs;
            ViewBag.CardCount = totalJobs;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int) Math.Ceiling(totalJobs/(double)pageSize);
            ViewBag.RecentPosts = _db.JobPosts
                .OrderBy(o => o.Id)
                .Reverse()
                .Take(3);
            ViewBag.Categories = _db.JobCategories
                .OrderBy(o => o.Id)
                .Reverse()
                .ToList();
            return View();
        }
        
        [HttpGet]
        public IActionResult ViewPost(int id) {
            var job = _db.JobPosts
                .Include(j => j.JobCategory)
                .FirstOrDefault(o => o.Id == id && o.IsActive == true);
            if (job == null) {
                TempData["error"] = "Record Not Found";
                return RedirectToAction("FindJobs");
            }
            ViewBag.RecentPosts = _db.JobPosts
                .OrderBy(o => o.Id)
                .Reverse()
                .Take(3);
            ViewBag.Post = job;
            ViewBag.Categories = _db.JobCategories
                .OrderBy(o => o.Id)
                .Reverse()
                .ToList();
            return View(job);
        }

        [HttpGet]
        public IActionResult Category(string Name, int page = 1) {
            int pageSize = 3;
            var jobPosts = _db.JobPosts
                .Include(j => j.JobCategory)
                .Where(o => o.IsActive == true && o.JobCategory.Name == Name.ToLower())
                .OrderBy(o => o.Id)
                .Reverse();
            System.Console.WriteLine($"Category Received: {Name} | Category Status: {_db.JobCategories.FirstOrDefault(o => o.Name == Name.ToLower()) == null}");
            // if (_db.JobCategories.FirstOrDefault(o => o.Name == Name.ToLower()) == null) {
            //     TempData["error"] = "No Records Found!";
            //     return RedirectToAction("FindJobs");
            // }
            int totalJobs = jobPosts.Count();
            var paginatedJobs = jobPosts.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            if (page > (int) Math.Ceiling(totalJobs/(double)pageSize) && totalJobs != 0) {
                TempData["error"] = "Record Not Found! Invalid Request";
                return RedirectToAction("FindJobs");
            }
            ViewBag.Category = Name;
            ViewBag.Cards = paginatedJobs;
            ViewBag.CardCount = totalJobs;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int) Math.Ceiling(totalJobs/(double)pageSize);
            ViewBag.RecentPosts = _db.JobPosts
                .OrderBy(o => o.Id)
                .Reverse()
                .Take(3);
            ViewBag.Categories = _db.JobCategories
                .OrderBy(o => o.Id)
                .Reverse()
                .ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Search(string query, int page = 1) {
            int pageSize = 3;
            var jobPosts = _db.JobPosts
                .Include(j => j.JobCategory)
                .Where(o => 
                    EF.Functions.Like(o.Title, '%' + query + '%') ||
                    EF.Functions.Like(o.Content, '%' + query + '%')
                )
                .OrderByDescending(o => o.Id)
                .Distinct();
            int totalJobs = jobPosts.Count();
            var paginatedJobs = jobPosts.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            if (page > (int) Math.Ceiling(totalJobs/(double)pageSize)) {
                TempData["error"] = "Record Not Found!";
                return RedirectToAction("FindJobs");
            }
            ViewBag.Cards = paginatedJobs;
            ViewBag.CardCount = totalJobs;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int) Math.Ceiling(totalJobs/(double)pageSize);
            ViewBag.Query = query;
            ViewBag.RecentPosts = _db.JobPosts
                .OrderBy(o => o.Id)
                .Reverse()
                .Take(3);
            ViewBag.Categories = _db.JobCategories
                .OrderBy(o => o.Id)
                .Reverse()
                .ToList();
            return View();
        }

        [HttpGet]
        public IActionResult ApplyNow(int post_id) {
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

                if (user.UserRole != "applicant") {
                    TempData["error"] = "You need a applicant account to perform this operation";
                    return RedirectToAction("Dashboard", "Auth");
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");

            }
            
            var post = _db.JobPosts
                .Include(o => o.JobCategory)
                .FirstOrDefault(o => o.Id == post_id);

            if (post == null) {
                TempData["error"] = "No Record Found!";
                return RedirectToAction("FindJobs");
            }

            var application_check = _db.JobApplications.FirstOrDefault(o => o.JobPost == post && o.User == user);
            if (application_check != null) {
                TempData["error"] = "You have already applied for this job post";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            ViewBag.JobPost = post;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyNow(int post_id, JobApplication obj, IFormFile cv) {
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

                if (user.UserRole != "applicant") {
                    TempData["error"] = "You need a applicant account to perform this operation";
                    return RedirectToAction("Dashboard", "Auth");
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");

            }
            
            var post = _db.JobPosts.Find(post_id);

            if (post == null) {
                TempData["error"] = "No Record Found!";
                return RedirectToAction("FindJobs");
            }
            
            var application_check = _db.JobApplications.FirstOrDefault(o => o.JobPost == post && o.User == user);
            if (application_check != null) {
                TempData["error"] = "You have already applied for this job post";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }

            if (cv.Length > 10 * 1000 * 1024) {
                TempData["error"] = "Resume/CV too large to process! File must be less than 10 MB";
                return View();
            }

            string[] allowedExtensions = {".pdf", ".docx", ".odt"};

            if (!allowedExtensions.Contains(Path.GetExtension(cv.FileName))) {
                TempData["error"] = "Invalid File Type! only .pdf, .docx or .odt files are allowed";
                return View();
            }

            string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(cv.FileName)}";
            string uploadsFolder = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "uploads/Resumes"
            );
            string filePath = Path.Combine(
                uploadsFolder,
                uniqueFileName
            );

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
                await cv.CopyToAsync(fileStream);
            
            _db.JobApplications.Add(new JobApplication{
                JobPost=post,
                User=user,
                CoverLetter=obj.CoverLetter,
                ResumeUrl=uniqueFileName
            });
            _db.SaveChanges();
            TempData["success"] = "Application Submitted! Good Luck";
            return RedirectToAction(
                "Dashboard",
                "Auth"
            );
        }

        public IActionResult EditApp(int app_id) {
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

                if (user.UserRole != "applicant") {
                    TempData["error"] = "You need a applicant account to perform this operation";
                    return RedirectToAction("Dashboard", "Auth");
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");

            }

            var app = _db.JobApplications.FirstOrDefault(o => o.Id == app_id);
            System.Console.WriteLine($"Application Record: {app} | check {app == null}");
            if (app == null) {
                TempData["error"] = "No Record Found!";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }

            if (app.User.Id != user.Id && !user.IsAdmin && !user.IsStaff) {
                TempData["error"] = "Invalid Operation! Request Failed";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            ViewBag.JobPost = _db.JobPosts
                .Include(o => o.JobCategory)
                .FirstOrDefault(o => o.Id == app.JobPostId);
            var request = _httpContextAccessor.HttpContext?.Request;
            var domain = $"{request?.Scheme}://{request?.Host}";
            ViewBag.PrevCV = $"{domain}/uploads/Resumes/{app.ResumeUrl}";
            return View(app);
        }

        [HttpPost]
        public async Task<IActionResult> EditApp(int app_id, JobApplication obj, IFormFile cv) {
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

                if (user.UserRole != "applicant") {
                    TempData["error"] = "You need a applicant account to perform this operation";
                    return RedirectToAction("Dashboard", "Auth");
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");

            }

            var app = _db.JobApplications
                    .Include(o => o.JobPost)
                    .FirstOrDefault(o => o.Id == app_id);
            if (app == null) {
                TempData["error"] = "No Record Found!";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }

            if (app.User.Id != user.Id && !user.IsAdmin && !user.IsStaff) {
                TempData["error"] = "Invalid Operation! Request Failed";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            string uniqueFileName = app.ResumeUrl;
            if (cv != null) {
                if (app.ResumeUrl != "" || app.ResumeUrl != null) {
                    string oldFilePath = Path.Combine(
                        Path.Combine(_webHostEnvironment.WebRootPath, "uploads/Resumes"),
                        app.ResumeUrl
                    );
                    if (System.IO.File.Exists(oldFilePath))
                        System.IO.File.Delete(oldFilePath);
                }
                if (cv.Length > 10 * 1000 * 1024) {
                    TempData["error"] = "Resume/CV too large to process! File must be less than 10 MB";
                    return View();
                }

                string[] allowedExtensions = {".pdf", ".docx", ".odt"};

                if (!allowedExtensions.Contains(Path.GetExtension(cv.FileName))) {
                    TempData["error"] = "Invalid File Type! only .pdf, .docx or .odt files are allowed";
                    return View(app);
                }

                uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(cv.FileName)}";
                string uploadsFolder = Path.Combine(
                    _webHostEnvironment.WebRootPath,
                    "uploads/Resumes"
                );
                string filePath = Path.Combine(
                    uploadsFolder,
                    uniqueFileName
                );

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                    await cv.CopyToAsync(fileStream);
                
            }
            app.ResumeUrl = uniqueFileName;
            app.CoverLetter = obj.CoverLetter;
            _db.JobApplications.Update(app);
            _db.SaveChanges();
            TempData["success"] = "Application is now edited!";
            return RedirectToAction(
                "Dashboard",
                "Auth"
            );
        }

        public IActionResult DeleteApp(int app_id) {
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

                if (user.UserRole != "applicant") {
                    TempData["error"] = "You need a applicant account to perform this operation";
                    return RedirectToAction("Dashboard", "Auth");
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");

            }

            var app = _db.JobApplications.FirstOrDefault(o => o.Id == app_id);
            System.Console.WriteLine($"Application Record: {app} | check {app == null}");
            if (app == null) {
                TempData["error"] = "No Record Found!";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }

            if (app.User.Id != user.Id && !user.IsAdmin && !user.IsStaff) {
                TempData["error"] = "Invalid Operation! Request Failed";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            ViewBag.JobPost = _db.JobPosts
                .Include(o => o.JobCategory)
                .FirstOrDefault(o => o.Id == app.JobPostId);
            return View();
        }
        [HttpPost]
        public IActionResult DeleteApp(int app_id, string password) {
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

                if (user.UserRole != "applicant") {
                    TempData["error"] = "You need a applicant account to perform this operation";
                    return RedirectToAction("Dashboard", "Auth");
                }
            } catch {
                Response.Cookies.Delete("AuthCookie");
                TempData["error"] = "Authentication Failed! Login To Continue!";
                return RedirectToAction("Login", "Auth");

            }

            var app = _db.JobApplications.FirstOrDefault(o => o.Id == app_id);
            if (app == null) {
                TempData["error"] = "No Record Found!";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }

            if (app.User.Id != user.Id && !user.IsAdmin && !user.IsStaff) {
                TempData["error"] = "Invalid Operation! Request Failed";
                return RedirectToAction(
                    "Dashboard",
                    "Auth"
                );
            }
            var check_password = BCryptHelper.CheckPassword(
                password,
                user.Password
            );
            if (!check_password) {
                ModelState.AddModelError(
                    "",
                    "Invalid Password!"
                );
                TempData["error"] = "Invalid Password! Try Again";
                return View();
            }

            if (app.ResumeUrl != "" || app.ResumeUrl != null) {
                string oldFilePath = Path.Combine(
                    Path.Combine(_webHostEnvironment.WebRootPath, "uploads/Resumes"),
                    app.ResumeUrl
                );
                if (System.IO.File.Exists(oldFilePath))
                    System.IO.File.Delete(oldFilePath);
            }

            _db.JobApplications.Remove(app);
            _db.SaveChanges();

            TempData["success"] = "Job Application Deleted Successfully!";
            return RedirectToAction(
                "Dashboard",
                "Auth"
            );
        }
    }
}

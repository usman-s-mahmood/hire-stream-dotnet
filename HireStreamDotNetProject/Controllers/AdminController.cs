// Created Manually!
// all admin related stuff like managing users, job details, etc.

using Microsoft.AspNetCore.Mvc;
using HireStreamDotNetProject.Models;
using HireStreamDotNetProject.Data;
using HireStreamDotNetProject.Utils;
using Microsoft.EntityFrameworkCore;

namespace HireStreamDotNetProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly TokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminController(ApplicationDbContext db, TokenService tk, IHttpContextAccessor httpContextAccessor) {
            _db = db;
            _tokenService = tk;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET: /Admin/
        public IActionResult Index()
        {
            return Redirect("/Admin/AdminPanel");
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
            JobCategory? check = _db.JobCategories.FirstOrDefault(o => o.Name == obj.Name.ToLower());
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

        public IActionResult AdminPanel() { // features to be included User Functions(View, Edit, Delete), Contact Query Cards, Job post cards(View, Edit, Delete), Job Application Cards(View, Edit, Delete)
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
            string profile_pic = " ";
            var request = _httpContextAccessor.HttpContext?.Request;
            var domain = $"{request?.Scheme}://{request?.Host}";

            if (user.ProfilePic != "")
                profile_pic = $"{domain}/uploads/ProfilePics/{user.ProfilePic}";
            else if (user.ProfilePic == "" && user.Gender == "Male")
                profile_pic = $"{domain}/assets/default-img/DefaultUserMale.png";
            
            else if (user.ProfilePic == "" && user.Gender == "Female")
                profile_pic = $"{domain}/assets/default-img/DefaultUserFemale.png";

            else if (user.ProfilePic == "" && user.Gender != "Male" && user.Gender != "Female")
                profile_pic = $"{domain}/assets/default-img/DefaultUserMale.png";
            System.Console.WriteLine($"Value of profile pic: {profile_pic} | Value of user profile pic: {user.ProfilePic == ""}");
            ViewBag.Email = user.Email;
            ViewBag.Username = user.Username;
            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            ViewBag.Gender = user.Gender;
            ViewBag.ProfilePic = profile_pic;
            ViewBag.UserRole = user.UserRole;
            ViewBag.IsAuth = true;
            ViewBag.Role = user.UserRole;
            ViewBag.SocialLink = user.SocialLink;
            ViewBag.AboutUser = user.AboutUser;
            return View();
        }

        public IActionResult UserDetails(int page=1, string query="") {
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
            var users = _db.Users.AsQueryable();

            if (!string.IsNullOrEmpty(query)) {
                users = users.Where(o => 
                    EF.Functions.Like(o.Username, $"%{query}%") ||
                    EF.Functions.Like(o.Email, $"%{query}%") ||
                    EF.Functions.Like(o.FirstName, $"%{query}%") ||
                    EF.Functions.Like(o.LastName, $"%{query}%") 
                );
                ViewBag.Query = query;
            }
            users = users.OrderByDescending(o => o.Id).Distinct();
            int pageSize = 3;
            int totalUsers = users.Count();
            var paginatedUsers = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.Users = paginatedUsers;
            ViewBag.TotalUsers = totalUsers;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int) Math.Ceiling(totalUsers/(double) pageSize);
            if (query != "")
                ViewBag.Query = query;
            return View();
        }
    
    }
}

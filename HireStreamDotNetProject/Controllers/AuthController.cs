// created manually!
// this controller will contain routes for login, logout, register, edit user, delete user, forgot password, etc.

using Microsoft.AspNetCore.Mvc;
using HireStreamDotNetProject.Data;
using HireStreamDotNetProject.Models;
using DevOne.Security.Cryptography.BCrypt;
using HireStreamDotNetProject.Utils;
using System.Net;

namespace HireStreamDotNetProject.Controllers
{
    public class AuthController : Controller
    {
        protected readonly ApplicationDbContext _db;
        protected readonly IConfiguration _config;
        protected readonly Utils.TokenService _tokenService;
        public AuthController(HireStreamDotNetProject.Data.ApplicationDbContext db, IConfiguration config, Utils.TokenService tokenService) {
            _db = db;
            _config = config;
            _tokenService = tokenService;
        }
        // GET: /Auth/
        public IActionResult Index()
        {
            return Redirect("Auth/Login");
        }

        [HttpGet]
        public IActionResult Login() {
            string? auth_token = Request.Cookies["AuthCookie"];
            System.Console.WriteLine($"auth_token==null: {auth_token == null}");
            if (auth_token != null) {
                TempData["error"] = "You are already logged in!";
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password) {
            string? auth_token = Request.Cookies["AuthCookie"];
            System.Console.WriteLine($"auth_token==null: {auth_token == null}");
            if (auth_token != null) {
                TempData["error"] = "You are already logged in!";
                return RedirectToAction("Dashboard");
            }
            User? user = _db.Users.FirstOrDefault(o => o.Email == email);
            if (user == null) {
                ModelState.AddModelError(
                    "",
                    "User Not Found!"
                );
                TempData["error"] = "User Not Found!";
                return View();
            }

            var check_password = BCryptHelper.CheckPassword(
                password,
                user.Password
            );

            if (check_password) {
                TempData["success"] = "You are now Logged In!";
                string jsonPayload = $"{{\"email\":\"{user.Email}\",\"username\":\"{user.Username}\"}}";
                string? token = _tokenService.GenerateToken(jsonPayload);
                System.Console.WriteLine($"AuthToken: {token}");
                CookieOptions options = new CookieOptions{
                    HttpOnly = false,
                    Expires = DateTime.UtcNow.AddHours(1)
                };
                Response.Cookies.Append(
                    "AuthCookie",
                    token,
                    options
                );
                return RedirectToAction("Dashboard");
            }
            else {
                ModelState.AddModelError(
                    "",
                    "Password Mismatch"
                );
                TempData["error"] = "Password Mismatch! Try Again";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Signup() {
            // string? auth_token = HttpContext.Session.GetString("AuthToken");
            // System.Console.WriteLine($"auth_token==null: {auth_token == null}");
            var auth_token = Request.Cookies["AuthCookie"];
            System.Console.WriteLine(auth_token);
            if (auth_token != null) {
                TempData["error"] = "You are already logged in!";
                return RedirectToAction("Dashboard");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Signup(string username, string first_name, string last_name, string email, string password, string gender) {
            var auth_token = Request.Cookies["AuthCookie"];
            System.Console.WriteLine(auth_token);
            if (auth_token != null) {
                TempData["error"] = "You are already logged in!";
                return RedirectToAction("Dashboard");
            }
            var salt = BCryptHelper.GenerateSalt(10);
            var hash = BCryptHelper.HashPassword(
                password,
                salt
            );
            var check = BCryptHelper.CheckPassword(
                password,
                hash
            );
            System.Console.WriteLine($"values received are:\n{username}\n{first_name}\n{last_name}\n{email}\n{password}\n{gender}\n{hash}\nPassword Check: {check}");
            User? email_obj = _db.Users.FirstOrDefault(o => o.Email == email);
            if (email_obj != null) {
                ModelState.AddModelError(
                    "",
                    "Invalid EMAIL, provide a unique email"
                );
                TempData["error"] = "Provid a unique email";
                return View();
            }
            User? username_obj = _db.Users.FirstOrDefault(o => o.Username == username);
            if (username_obj != null) {
                ModelState.AddModelError(
                    "",
                    "Invalid Username, provide a unique username"
                );
                TempData["error"] = "Provid a unique username";
                return View();
            }
            if (ModelState.IsValid) {
                _db.Users.Add(new Models.User {
                    Username = username,
                    FirstName = first_name,
                    LastName = last_name,
                    Email = email,
                    Gender = gender,
                    Password = hash
                });
                _db.SaveChanges();
                TempData["success"] = "You are now registered!";
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult EditUser() {
            return View();
        }

        public IActionResult DeleteUser() {
            return View(); // this also does not require any view so use something like a redirect for this
        }

        public IActionResult ForgotPassword() {
            return View();
        }

        public IActionResult CreateProfile() {
            return View();
        }

        public IActionResult EditProfile() {
            return View();
        }

        [HttpGet]
        public IActionResult Dashboard() {
            string? auth_token = Request.Cookies["AuthCookie"];
            System.Console.WriteLine(auth_token);
            if (auth_token == null) {
                TempData["error"] = "Login to continue!";
                return RedirectToAction("Login");
            }
            var payload = _tokenService.DecryptToken(auth_token);
            // System.Console.WriteLine(payload);

            var data = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(payload);

            string? email = data["email"];
            string? username = data["username"];

            User? user = _db.Users.FirstOrDefault(o => o.Email == email && o.Username == username);

            if (user == null) {
                HttpContext.Session.Remove("AuthToken");
                TempData["error"] = "Login To Continue";
                return RedirectToAction("Login");
            }
            
            string profile_pic = " ";

            if (user.ProfilePic != "")
                profile_pic = user.ProfilePic;
            else if (user.ProfilePic == "" && user.Gender == "Male")
                profile_pic = "~/assets/default-img/DefaultUserMale.png";
            
            else if (user.ProfilePic == "" && user.Gender == "Female")
                profile_pic = "~/assets/default-img/DefaultUserFemale.png";

            else if (user.ProfilePic == "" && user.Gender != "Male" && user.Gender != "Female")
                profile_pic = "~/assets/default-img/DefaultUserMale.png";
            System.Console.WriteLine($"Value of profile pic: {profile_pic} | Value of user profile pic: {user.ProfilePic == ""}");
            ViewBag.Email = user.Email;
            ViewBag.Username = user.Username;
            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            ViewBag.Gender = user.Gender;
            ViewBag.ProfilePic = profile_pic;
            ViewBag.IsAuth = true;

            return View();
        }

        public IActionResult Logout() {
            string? auth_token = Request.Cookies["AuthCookie"];
            System.Console.WriteLine(auth_token);
            if (Request.Cookies["AuthCookie"] != null) {
                Response.Cookies.Delete("AuthCookie");
                TempData["success"] = "You are now logged out!";
                return RedirectToAction("Login");
            }
            TempData["error"] = "Login First to logout!";
            return RedirectToAction("Login");
        }
    }
}

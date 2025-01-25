// created manually!
// this controller will contain routes for login, logout, register, edit user, delete user, forgot password, etc.

using Microsoft.AspNetCore.Mvc;
using HireStreamDotNetProject.Data;
using HireStreamDotNetProject.Models;
using DevOne.Security.Cryptography.BCrypt;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using JWT;

namespace HireStreamDotNetProject.Controllers
{
    public class AuthController : Controller
    {
        protected readonly ApplicationDbContext _db;

        public AuthController(HireStreamDotNetProject.Data.ApplicationDbContext db) {
            _db = db;
        }
        // GET: /Auth/
        public IActionResult Index()
        {
            return Redirect("Auth/Login");
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password) {
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
            return View();
        }

        [HttpGet]
        public IActionResult Signup() {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(string username, string first_name, string last_name, string email, string password, string gender) {
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

        public IActionResult Dashboard() {
            return View();
        }

        public IActionResult Logout() {
            return View(); // this does not need a view, so use something like a redirect
        }
    }
}

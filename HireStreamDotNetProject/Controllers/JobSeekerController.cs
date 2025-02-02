// Created Manually!
// dedicated for users who want to search for jobs. This will include job posts, search section, job application forms, etc.

using Microsoft.AspNetCore.Mvc;
using HireStreamDotNetProject.Data;
using HireStreamDotNetProject.Utils;
using HireStreamDotNetProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HireStreamDotNetProject.Controllers
{
    public class JobSeekerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly TokenService _tokenService;
        public JobSeekerController(ApplicationDbContext db, TokenService tokenService) {
            _db = db;
            _tokenService = tokenService;
        }
        // GET: /JobSeeker/
        public IActionResult Index()
        {
            return View();
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
            return View(job);
        }
    }
}

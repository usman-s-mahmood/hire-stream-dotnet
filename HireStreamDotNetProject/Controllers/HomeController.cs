using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HireStreamDotNetProject.Models;
using HireStreamDotNetProject.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using HireStreamDotNetProject.Utils;
using System.Text.Json;

namespace HireStreamDotNetProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;
    private readonly EmailService _em;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, EmailService em)
    {
        _logger = logger;
        _db = db;
        _em = em;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About() {
        return View();
    }

    public IActionResult Contact() {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Contact(Contact obj) {
        bool sent;
        try {
            await _em.SendEmailAsync(
                "muhammadusman27virgo@yahoo.com",
                $"Contact Query From HireStream by {obj.Name}",
                obj.Message
            );
            string message = $"Dear {obj.Name}\nYour query has been submitted! our represntative will soon contact you in this regard\nBest Regards,\nIT Team Hire Stream";
            await _em.SendEmailAsync(
                obj.Email,
                "Contact Query Submitted at HireStream",
                message
            );
            sent = true;
            Console.WriteLine("Email Sent!");
            TempData["success"] = "Your query is submitted and we will contact you soon";
        } catch {
            sent = false;
            Console.WriteLine("Email Not Sent!");
            TempData["success"] = "Query Submitted Successfully!";
        }
        _db.Contacts.Add(new Models.Contact {
            Name = obj.Name,
            Email = obj.Email,
            Message = obj.Message,
            IsSent = sent
        });
        _db.SaveChanges();
        return View();
    }
    public IActionResult Services() {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

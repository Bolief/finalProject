using finalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Area("User")] // Specifies that this controller belongs to the "User" area
public class UserController : Controller
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
        return View("~/Areas/User/Views/Users/Login.cshtml");
    }

    [HttpPost]
    public IActionResult Login(User model)
    {
        if (ModelState.IsValid)
        {
            // Find the user by username
            var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);

            if (user != null)
            {
                // Successful login logic
                // Redirect to Home/Index within the User area
                return RedirectToAction("Index", "Home", new { area = "User" });
            }

            // Add an error if the user is not found
            ModelState.AddModelError("Username", "User not found.");
        }
        return View(model);
    }


    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User model)
    {
        if (ModelState.IsValid)
        {
            // Check if the username already exists
            if (_context.Users.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "Username is already taken.");
                return View(model);
            }

            var newUser = new User
            {
                Username = model.Username,
                TotalWins = 0, // New users start with 0 wins
                Teams = new List<Team>(), // Empty team list
                Badges = new List<Badge>() // Empty badge list
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("Login", "User", new { area = "User" });
        }
        return View(model);
    }
}

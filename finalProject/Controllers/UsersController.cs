using finalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class UsersController : Controller
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var users = _context.Users.Include(u => u.Teams).ToList();
        return View(users);
    }

    //Login and Registration Methods

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Implement authentication logic
            return RedirectToAction("Index");
        }
        return View(model);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var newUser = new User
            {
                Username = model.Username,
                TotalWins = 0, // New users start with 0 wins
                Teams = new List<Team>(), // Empty team list
                Badges = new List<Badge>() // Empty badge list
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
        return View(model);
    }

}




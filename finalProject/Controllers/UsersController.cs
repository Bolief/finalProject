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
}


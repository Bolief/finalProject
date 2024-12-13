using finalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace finalProject.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
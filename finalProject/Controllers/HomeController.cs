using finalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace finalProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

}
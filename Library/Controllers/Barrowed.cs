using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class Barrowed : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

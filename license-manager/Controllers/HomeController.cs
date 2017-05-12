using Microsoft.AspNetCore.Mvc;

namespace licensemanager.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        public IActionResult Index()
        {
            return View();
        }
    }
}

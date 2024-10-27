using Microsoft.AspNetCore.Mvc;

namespace WEB.Areas.Education.Controllers
{
    [Area("Education")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

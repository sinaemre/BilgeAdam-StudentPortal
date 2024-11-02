using Microsoft.AspNetCore.Mvc;

namespace WEB.Areas.Education.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

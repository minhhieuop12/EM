using Microsoft.AspNetCore.Mvc;

namespace EM.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

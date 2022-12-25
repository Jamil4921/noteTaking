using Microsoft.AspNetCore.Mvc;

namespace noteTaking.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

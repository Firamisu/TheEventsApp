using Microsoft.AspNetCore.Mvc;

namespace TheEventsApp.Controllers
{
    public class StatusCodeController : Controller
    {
        // GET: StatusCode
        public async Task<IActionResult> Index(int? code)
        {
            return View("Index", code);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Scanton.Models;

namespace Scanton.Controllers
{

    public class ViewCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ViewCartController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [Route("view-orders")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

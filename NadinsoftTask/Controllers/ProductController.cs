using Microsoft.AspNetCore.Mvc;
using NadinsoftTask.Models;
using System.Collections;

namespace NadinsoftTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
    }
}

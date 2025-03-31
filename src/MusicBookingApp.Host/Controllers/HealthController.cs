using Microsoft.AspNetCore.Mvc;

using MusicBookingApp.Host.Controllers.Base;

namespace MusicBookingApp.Host.Controllers
{
    public class HealthController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello World!");
        }
    }
}
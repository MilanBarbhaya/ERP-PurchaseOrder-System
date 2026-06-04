using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok("Authorized User");
        }

        [HttpGet("finance")]
        [Authorize(Roles = "FinanceManager")]
        public IActionResult Finance()
        {
            return Ok("Finance Manager Only");
        }
    }
}

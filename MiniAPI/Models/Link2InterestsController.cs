namespace MiniAPI.Models
{
    using Microsoft.AspNetCore.Mvc;
    using MiniAPI.Models;

    [ApiController]
    [Route("api/[controller]")]
    public class Link2InterestsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateLink([FromBody] Link2Interests link2Interest)
        {
            return Ok("Link created successfully");
        }
    }
}
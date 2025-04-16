using Microsoft.AspNetCore.Mvc;

namespace CloudCustomer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUSersService _usersService;

        public UsersController(IUSersService userService)
        {
            this._usersService = userService;
        }
        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> Get()
        {
            var users = await _usersService.GetAllUsers();
            if (users.Any())
                return Ok(users);
            return NotFound();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGKHandIn3.Models;
using NGKHandIn3.Services;
using static BCrypt.Net.BCrypt;

namespace NGKHandIn3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private IUserService _userService;
        private const int BcryptWorkfactor = 10;

        public UsersController(ApplicationContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
            _userService.Context = _context;
        }

        [HttpPost("Authenticate"), AllowAnonymous]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpPost("Register"), AllowAnonymous]
        public async Task<ActionResult<User>> Register(User user)
        {
            user.Username = user.Username.ToLower();
            var userNameExist = await _context.Users.Where(u => u.Username == user.Username).FirstOrDefaultAsync();
            if (userNameExist != null)
                return BadRequest(new {ErrorMessage = "That username is already taken"});

            // Der mangler noget kode her. Dette kunne eventuelt også være i UserService.
            // Se "ASP Core Authentication & Authorisation" slide 12 fra lektion 12 for hjælp.
            // HUSK! at ændre returtypen til en eller anden "TokenDto". Samt ændre parametren til hvad end Register / Login skal tage.

            return null;
        }

        [HttpPost("Login"), AllowAnonymous]
        public async Task<ActionResult<User>> Login(User user)
        {
            // Se kommentarer i "Register".
            return null;
        }
    }
}

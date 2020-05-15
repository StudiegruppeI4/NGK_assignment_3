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
using NGKHandIn3.Utilities;
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
            var user = _userService.Authenticate(userParam.Email, userParam.Email);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpPost("Register"), AllowAnonymous]
        public async Task<ActionResult<Token>> Register(RegisterUser newUser)
        {
            newUser.Email = newUser.Email.ToLower();
            var emailExist = await _context.Users.Where(u => u.Email == newUser.Email).FirstOrDefaultAsync();
            if (emailExist != null)
                return BadRequest(new { ErrorMessage = "That email is already taken" });

            var user = new User
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                PasswordHash = HashPassword(newUser.Password, BcryptWorkfactor)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var jwtToken = new Token();
            jwtToken.JWT = JWTUtilities.GenerateToken(user.FirstName, user.LastName, user.Email, user.UserId);
            return Ok(new { user = user.UserId, token = jwtToken});
        }

        [HttpPost("Login"), AllowAnonymous]
        public async Task<ActionResult<Token>> Login(LoginUser login)
        {
            login.Email = login.Email.ToLower();
            var user = await _context.Users.Where(u => u.Email == login.Email).FirstOrDefaultAsync();
            if (user != null)
            {
                var validPassword = Verify(login.Password, user.PasswordHash);
                if (validPassword)
                {
                    return new Token() {JWT = JWTUtilities.GenerateToken(user.FirstName, user.LastName, user.Email, user.UserId)};
                }
            }
            ModelState.AddModelError(string.Empty, "Wrong email or password");
            return BadRequest(ModelState);
        }
    }
}

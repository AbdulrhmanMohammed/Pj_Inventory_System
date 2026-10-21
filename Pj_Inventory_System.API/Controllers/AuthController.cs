using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Application.Dtos;
using Pj_Inventory_System.Application.Services.Base;
using Pj_Inventory_System.Domain.Models;
using Pj_Inventory_System.Infrastructure.Data;

namespace Pj_Inventory_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            User? user = _userService.Login(loginDto);

            if (user != null)
            {
                return Ok(new
                {
                    userid = user.Id,
                    username = user.Username,
                    name = user.Name,
                    email = user.Email,
                    message = "Login Successful"
                });
            }
            else
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password"
                });
            }
        }




        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            if (string.IsNullOrWhiteSpace(registerDto.Name) ||
                string.IsNullOrWhiteSpace(registerDto.Email) ||
                string.IsNullOrWhiteSpace(registerDto.Username) ||
                string.IsNullOrWhiteSpace(registerDto.Password))
            {
                return BadRequest(new
                {
                    message = "All fields are required"
                });
            }

            if (registerDto.Password.Length < 6)
            {
                return BadRequest(new
                {
                    message = "Password must be at least 6 characters"
                });
            }

            User? user = _userService.Register(registerDto);

            if (user != null)
            {
                return Ok(new
                {
                    message = "Registration Successful"
                });
            }

            return Conflict(new
            {
                message = "Username or Email already exists"
            });
        }


    }
}

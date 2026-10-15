using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Domain.Models;
using Pj_Inventory_System.Infrastructure.Data;
using Pj_Inventory_System.Application.Dtos;

namespace Pj_Inventory_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly InventorySystemDbContext _context;

        public UsersController(InventorySystemDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "اسم المستخدم غير موجود"
                });
            }

            bool passwordMatch = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!passwordMatch)
            {
                return Unauthorized(new
                {
                    message = "كلمة المرور غير صحيحة"
                });
            }

            return Ok(new
            {
                message = "تم تسجيل الدخول بنجاح",
                username = user.Username,
                name = user.Name,
                uid = user.UID
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            // Check empty fields
            if (string.IsNullOrWhiteSpace(dto.Name) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Username) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest(new { message = "Please fill in all fields" });
            }

            // Password length check
            if (dto.Password.Length < 6)
            {
                return BadRequest(new { message = "Password must be at least 6 characters" });
            }

            // Check email
            var emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (emailExists)
            {
                return BadRequest(new { message = "Email is already in use" });
            }

            // Check username
            var usernameExists = await _context.Users.AnyAsync(u => u.Username == dto.Username);
            if (usernameExists)
            {
                return BadRequest(new { message = "Username is already taken" });
            }

            // Create user
            var user = new User
            {
                UID = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Email = dto.Email,
                Username = dto.Username,

                // 🔥 هنا نحفظ كلمة المرور الأصلية
                Password = dto.Password,

                // 🔥 وهنا نحفظ الـ Hash
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Account created successfully" });
        }


    }
}

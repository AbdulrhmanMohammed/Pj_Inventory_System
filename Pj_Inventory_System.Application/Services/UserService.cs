
using Pj_Inventory_System.Application.Dtos;
using Pj_Inventory_System.Application.Services.Base;
using Pj_Inventory_System.Domain.Models;
using Pj_Inventory_System.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pj_Inventory_System.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User? Login(LoginDto loginDto)
        {
            var user = _unitOfWork.UserRepo
                .GetByUsername(loginDto.Username);

            if (user == null ||
                string.IsNullOrEmpty(user.PasswordHash))
            {
                return null;
            }

            bool userFound = BCrypt.Net.BCrypt.Verify(
                loginDto.Password,
                user.PasswordHash
            );

            return userFound ? user : null;

            //if(userFound)
            //{
            //    return user;
            //}
            //else
            //{
            //    return null;
            //}



        }



        public User? Register(RegisterDto registerDto)
        {
            var existingUsername = _unitOfWork.UserRepo
                 .GetByUsername(registerDto.Username);

            var existingEmail = _unitOfWork.UserRepo
                .GetByEmail(registerDto.Email);

            if (existingUsername != null || existingEmail != null)
            {
                return null;
            }

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Username = registerDto.Username,
                Password = registerDto.Password,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(
                    registerDto.Password
                )
            };

            _unitOfWork.UserRepo.Add(user);

            _unitOfWork.Save();

            return user;
        }








    }
}

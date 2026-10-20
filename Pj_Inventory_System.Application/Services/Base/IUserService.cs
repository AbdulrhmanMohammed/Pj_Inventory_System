using Pj_Inventory_System.Application.Dtos;
using Pj_Inventory_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pj_Inventory_System.Application.Services.Base
{
    public interface IUserService
    {
        User? Login(LoginDto loginDto);

        User? Register(RegisterDto registerDto);

    }
}

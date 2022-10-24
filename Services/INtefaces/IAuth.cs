using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.INtefaces
{
    public interface IAuth
    {
        Task<Authentication> RegisterAsync(RegisterModel rm);
        Task<Authentication> LogInAsync(LoginModel lm);
    }
}

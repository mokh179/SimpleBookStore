using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IApplication.IAppServices
{
    public interface IAuthentication
    {
        Task<Authentication> RegisterAsync(RegisterModel rm);
        Task<Authentication> LogInAsync(LoginModel lm);
    }
}

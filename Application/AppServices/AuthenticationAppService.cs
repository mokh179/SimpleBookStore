using Services.INtefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class AuthenticationAppService :IAuthentication
    {
        public IAuth _Authrntication;

        public AuthenticationAppService(IAuth Authrntication)
        {
            _Authrntication = Authrntication;   
        }

        public async Task<Authentication> LogInAsync(LoginModel lm)
        {
            return await _Authrntication.LogInAsync(lm);
        }

        public async Task<Authentication> RegisterAsync(RegisterModel rm)
        {
           return await _Authrntication.RegisterAsync(rm);
        }
    }
}

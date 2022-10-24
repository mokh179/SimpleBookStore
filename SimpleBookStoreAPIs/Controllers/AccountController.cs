using Common.DTOs;
using IApplication.IAppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBookStoreAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAuthentication  _authenticationService;
        public AccountController(IAuthentication authenticationService)
        {
            _authenticationService= authenticationService;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> Register([FromBody] RegisterModel rm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _authenticationService.RegisterAsync(rm);
            if (!res.IsAuthenticated)
                return BadRequest(res.Message);
            return Ok(res);
        }

      

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn([FromBody] LoginModel lm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _authenticationService.LogInAsync(lm);
            if (!res.IsAuthenticated)
                return BadRequest(res.Message);
            return Ok(res);
        }
    }
}

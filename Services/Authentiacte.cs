using Common.DTOs;
using Core.Const;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.INtefaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class Authentiacte : IAuth
    {
        private readonly JWT _jwt;
        private readonly UserManager<User> _user;
        private readonly RoleManager<IdentityRole> _role;
        private readonly IUnitOfWork _unitOfWork;


        public Authentiacte(UserManager<User> user, IOptions<JWT> jwt, RoleManager<IdentityRole> role,IUnitOfWork unitOfWork)
        {
            _user = user;
            _jwt = jwt.Value;
            _role = role;
            _unitOfWork = unitOfWork;
        }

        private async Task<JwtSecurityToken> Generatetoken(User user)
        {
            var Userclaims = await _user.GetClaimsAsync(user); //get user claims
            var Userroles = await _user.GetRolesAsync(user); //get user rols
            var Roleclaims = new List<Claim>();
            foreach (var item in Userroles)
            {
                Roleclaims.Add(new Claim("roles", item));
            }
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //New ID for each claim
                 new Claim(JwtRegisteredClaimNames.Email, user.Email),
                 new Claim("uid", user.Id)
            }.Union(Userclaims).Union(Roleclaims);
            var symmetrickey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingcredentials = new SigningCredentials(symmetrickey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
             issuer: _jwt.Issuer,
             audience: _jwt.Audience,
             claims: claims,
             expires: DateTime.Now.AddDays(_jwt.DurationInDay),
             signingCredentials: signingcredentials);
            return jwtSecurityToken;
        }


        public async Task<Authentication> LogInAsync(LoginModel lm)
        {
            throw new NotImplementedException();
            var user = lm.Username.Contains('@') ? await _user.FindByEmailAsync(lm.Username) : await _user.FindByNameAsync(lm.Username);

            var ValidPassword = await _user.CheckPasswordAsync(user, lm.Password);
            
                if (user == null || !ValidPassword)
                {
                    return new Authentication { Message = "Invalid Username or Password" };

                }
                else
                {
                    var token = await Generatetoken(user);

                    var userID = await _unitOfWork.Users.find(a => a.UserName == lm.Username);
                    //var RoleID = await _unitOfWork.UserRoles.Where(a => a.UserId == userID).Select(a => a.RoleId).FirstOrDefaultAsync();

                    //var role = _db.Roles.Where(a => a.Id == RoleID).Select(a => a.Name);
                    var Token = new JwtSecurityTokenHandler().WriteToken(token);
                    
                    Authentication auth = new Authentication
                    {
                        Username = user.UserName,
                        Tokenlife = token.ValidTo,
                        IsAuthenticated = true,
                        //Roles = await _db.Roles.Where(a => a.Id == RoleID).Select(a => a.Name).ToListAsync(),
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        userID = user.Id,

                    };

                    return auth;
                }
         
        }

        public async Task<Authentication> RegisterAsync(RegisterModel rm)
        {
            if (await _user.FindByEmailAsync(rm.Email) != null)
                return new Authentication { Message = "E-mail already Existed" };
            if (await _user.FindByNameAsync(rm.Username) != null)
                return new Authentication { Message = "UserName already Existed" };
            var user = new User()
            {
                FirstName = rm.Firstname,
                LastName = rm.Lastname,
                Email = rm.Email,
                UserName = rm.Username,  
                PhoneNumber = rm.phone
            };
            var result = await _user.CreateAsync(user, rm.Password);
            if (!result.Succeeded)
            {
                var errs = "";
                foreach (var item in result.Errors)
                {
                    errs = $"{item.Description} , ";
                }
                return new Authentication { Message = errs };
            }
            var token = await Generatetoken(user);
            await _user.AddToRoleAsync(user, "User");

            return new Authentication
            {
                email = user.Email,
                Tokenlife = token.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = user.UserName,
                userID = user.Id,
            };
        }
    }
}

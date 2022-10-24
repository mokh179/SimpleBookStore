using Application.AppServices;
using Common.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.INtefaces;
using Services.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Registeration
{
    public static class Register
    {
        public static void RegisterAppLicationServices( this IServiceCollection _srv)
        {
            _srv.AddScoped<IUnitOfWork, UnitOfWork>();
            _srv.AddScoped<IBookMapper, BookMapper>();
            _srv.AddScoped<IBookAppService, BookAppService>();
            _srv.AddScoped<IAuth,Authentiacte>();
            _srv.AddScoped<IAuthentication, AuthenticationAppService>();
        }
    }
}

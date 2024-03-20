﻿using Core.Security.JWT;
using Core.Security.OtpAuthenticator;
using Core.Security.OtpAuthenticator.OtpNet;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security;

public static class SecurityServiceRegistration
{
    public static IServiceCollection AddSecurityHelper(this IServiceCollection services)
    {
        services.AddScoped<ITokenHelper, JwtHelper>();
//        services.AddScoped<IEmailAuthenticatorHelper,EmailAuthenticatorHelper>();
        services.AddScoped<IOtpAuthenticator, OtpNetOtpAuthenticatorHelper>();

        return services;
    }

}

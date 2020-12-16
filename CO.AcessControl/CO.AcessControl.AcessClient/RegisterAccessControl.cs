using System;
using System.Text;
using CO.AcessControl.Core.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CO.AcessControl.AcessClient
{
    public static class RegisterAccessControl
    {
        public static void ConfigureAccessControl(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IUserService, UserService>();
            
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWTSECRET"))),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
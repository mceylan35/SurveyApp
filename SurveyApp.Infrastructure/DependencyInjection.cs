using Microsoft.Extensions.DependencyInjection;
using SurveyApp.Application.Common.Interfaces.Repositories;
using SurveyApp.Application.Common.Interfaces;
using SurveyApp.Application.Common.Interfaces.Services;
using SurveyApp.Infrastructure.Authentication;
using SurveyApp.Infrastructure.Persistence.MongoDb;
using SurveyApp.Infrastructure.Persistence.Repositories;
using SurveyApp.Infrastructure.Security;
using SurveyApp.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
   
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace SurveyApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {


            services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
            services.Configure<MongoDbSettings>(
             configuration.GetSection("MongoDbSettings"));

            services.AddSingleton<IMongoDbContext, MongoDbContext>();
             
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<IOptionRepository, OptionRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();

         
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

           
            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });

          
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}

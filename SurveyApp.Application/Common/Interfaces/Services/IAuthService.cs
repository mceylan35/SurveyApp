using SurveyApp.Application.Common.Results;
using SurveyApp.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Common.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> RegisterAsync(string email, string password);
        Task<Result<AuthResponse>> LoginAsync(string email, string password); 
    }
}

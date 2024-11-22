using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Auth
{
    public record AuthResponse(Guid UserId, string Email, string Token);
}

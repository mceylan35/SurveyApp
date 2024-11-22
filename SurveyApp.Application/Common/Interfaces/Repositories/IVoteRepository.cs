using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Common.Interfaces.Repositories
{
    public  interface IVoteRepository:IBaseRepository<Vote>
    {
        Task<List<Vote>> GetBySurveyIdAsync(Guid surveyId);
        Task<List<Vote>> GetByUserIdAsync(Guid userId);
        Task<bool> HasUserVotedAsync(Guid surveyId, Guid userId);
        Task AddVoteAsync(Vote vote);
    }
}

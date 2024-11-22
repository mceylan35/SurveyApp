using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Common.Interfaces.Repositories
{
    public interface IOptionRepository:IBaseRepository<Option>
    {
        Task<List<Option>> GetBySurveyIdAsync(Guid surveyId);
        Task CreateOptionsForSurvey(Guid surveyId, List<string> optionTexts);
        Task IncrementVoteCount(Guid optionId);
    }
}

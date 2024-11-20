using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Common.Interfaces.Repositories
{
    public interface ISurveyRepository : IBaseRepository<Survey>
    {
        Task<IEnumerable<Survey>> GetAllWithOptionsAsync();
        Task<Survey> GetByIdWithOptionsAsync(Guid id);
    }
}

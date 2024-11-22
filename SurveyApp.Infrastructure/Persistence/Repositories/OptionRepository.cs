using MongoDB.Driver;
using SurveyApp.Application.Common.Interfaces;
using SurveyApp.Application.Common.Interfaces.Repositories;
using SurveyApp.Domain.Entities;
using SurveyApp.Infrastructure.Extensions;
using SurveyApp.Infrastructure.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Persistence.Repositories
{
    public class OptionRepository : BaseRepository<Option>, IOptionRepository
    {
        public OptionRepository(IMongoDbContext context) : base(context)
        {
        }

        public async Task<List<Option>> GetBySurveyIdAsync(Guid surveyId)
        {
            return await Collection
            .Find(o => o.SurveyId == surveyId)
            .ToListAsync();
        }
        public async Task CreateOptionsForSurvey(Guid surveyId, List<string> optionTexts)
        {
            var options = optionTexts.Select(text => Option.Create(text, surveyId)).ToList();
            await Collection.InsertManyAsync(options);
        }

        public async Task IncrementVoteCount(Guid optionId)
        {
            var update = Builders<Option>.Update.Inc(x => x.VoteCount, 1);
            await Collection.UpdateOneAsync(x => x.Id == optionId, update);
        }
    }
}

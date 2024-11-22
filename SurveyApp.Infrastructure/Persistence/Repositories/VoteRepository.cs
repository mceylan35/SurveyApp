using MongoDB.Driver;
using SurveyApp.Application.Common.Interfaces;
using SurveyApp.Application.Common.Interfaces.Repositories;
using SurveyApp.Domain.Entities;
using SurveyApp.Infrastructure.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Persistence.Repositories
{
    public class VoteRepository : BaseRepository<Vote>, IVoteRepository
    {
        private readonly IMongoCollection<Survey> _surveyCollection;
        public VoteRepository(IMongoDbContext context ) : base(context)
        {
            _surveyCollection= context.GetCollection<Survey>(); 
        }

        public async Task<List<Vote>> GetBySurveyIdAsync(Guid surveyId)
        {
            return await Collection
                .Find(v => v.SurveyId == surveyId)
                .ToListAsync();
        }

        public async Task<List<Vote>> GetByUserIdAsync(Guid userId)
        {
            return await Collection
                .Find(v => v.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> HasUserVotedAsync(Guid surveyId, Guid userId)
        {
            var vote = await Collection
                .Find(v => v.SurveyId == surveyId && v.UserId == userId)
                .FirstOrDefaultAsync();
            return vote != null;
        }

        public async Task AddVoteAsync(Vote vote)
        {
            await Collection.InsertOneAsync(vote); 
        }
    }
}

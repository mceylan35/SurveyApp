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
    public class SurveyRepository : BaseRepository<Survey>, ISurveyRepository
    {
        public SurveyRepository(IMongoDbContext context) : base(context)
        {
        }

        public async Task<(List<Survey> Surveys, int TotalCount)> GetPaginatedListAsync(int pageNumber, int pageSize)
        {
            var totalCount = await Collection.CountDocumentsAsync(_ => true);

            var surveys = await Collection
                .Find(_ => true)
                .SortByDescending(s => s.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return (surveys, (int)totalCount);
        }
        public async Task<IEnumerable<Survey>> GetAllWithOptionsAsync()
        {
            return await Collection.Find(_ => true)
                .ToListAsync();
        }

      

        public async Task<Survey> GetByIdWithOptionsAsync(Guid id)
        {
            return await Collection.Find(s => s.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}

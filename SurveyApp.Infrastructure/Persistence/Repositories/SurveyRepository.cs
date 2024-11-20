using MongoDB.Driver;
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
        public SurveyRepository(IMongoDatabase database)
            : base(database, "Surveys") { }

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

using MongoDB.Driver;
using SurveyApp.Application.Common;
using SurveyApp.Application.Common.Interfaces;
using SurveyApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Persistence.Repositories.Base
{
    public abstract class BaseRepository<T> where T : MongoEntity
    {
        protected readonly IMongoCollection<T> Collection;

        protected BaseRepository(IMongoDbContext context)
        {
            Collection = context.GetCollection<T>();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await Collection.DeleteOneAsync(e => e.Id == id);
        }
    }
}

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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IMongoDbContext context) : base(context)
        {
        } 

        public async Task<User> GetByEmailAsync(string email)
        {
            return await Collection
                                  .Find(u => u.Email == email)
                                  .FirstOrDefaultAsync();
        }
    }
}

using MongoDB.Driver;
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
        public UserRepository(IMongoDatabase database)
            : base(database, "Users") { }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await Collection.Find(u => u.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}

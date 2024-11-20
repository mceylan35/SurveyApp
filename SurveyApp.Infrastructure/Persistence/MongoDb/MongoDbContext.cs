using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Persistence.MongoDb
{
    public class MongoDbContext : IApplicationDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoClient _client;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            _client = new MongoClient(settings.Value.ConnectionString);
            _database = _client.GetDatabase(settings.Value.DatabaseName);

            Users = _database.GetCollection<User>("Users");
            Surveys = _database.GetCollection<Survey>("Surveys");
            Options = _database.GetCollection<Option>("Options");
            Votes = _database.GetCollection<Vote>("Votes");
        }

        public IMongoCollection<User> Users { get; }
        public IMongoCollection<Survey> Surveys { get; }
        public IMongoCollection<Option> Options { get; }
        public IMongoCollection<Vote> Votes { get; }
    }
}

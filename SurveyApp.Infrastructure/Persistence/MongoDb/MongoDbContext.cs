using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using MongoDB.Driver;
using SurveyApp.Application.Common;
using SurveyApp.Application.Common.Interfaces;
using SurveyApp.Domain.Common;
using SurveyApp.Domain.Entities;
using SurveyApp.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Persistence.MongoDb
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;
         
        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(settings.Value.DatabaseName);
           // ConfigureIndexes();

        }
       
        public IMongoCollection<T> GetCollection<T>() where T : MongoEntity
        {
            return _database.GetCollection<T>(GetCollectionName<T>());
        }
        private static string GetCollectionName<T>()
        {
            return typeof(T).Name.ToLowerInvariant() + "s";
        }
        public IMongoCollection<User> Users { get; }
        public IMongoCollection<Survey> Surveys { get; }
        public IMongoCollection<Option> Options { get; }
        public IMongoCollection<Vote> Votes { get; }
       
        private void ConfigureIndexes()
        {
       
            var userIndexes = Users.Indexes;
            var emailIndex = new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(u => u.Email),
                new CreateIndexOptions { Unique = true });
            userIndexes.CreateOne(emailIndex);

            
            var voteIndexes = Votes.Indexes;
            var userSurveyIndex = new CreateIndexModel<Vote>(
                Builders<Vote>.IndexKeys
                    .Ascending(v => v.UserId)
                    .Ascending(v => v.SurveyId),
                new CreateIndexOptions { Unique = true });
            voteIndexes.CreateOne(userSurveyIndex);



            var surveyIndexes = Surveys.Indexes;

          
            var titleIndex = new CreateIndexModel<Survey>(
                Builders<Survey>.IndexKeys
                    .Text(s => s.Title)
            );

            
            var dateCreatorIndex = new CreateIndexModel<Survey>(
                Builders<Survey>.IndexKeys
                    .Ascending(s => s.CreatedAt)
                    .Ascending(s => s.CreatorId)
            ); 
            surveyIndexes.CreateMany(new[] { titleIndex, dateCreatorIndex });
        }
    }
}

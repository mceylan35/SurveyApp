using MongoDB.Bson.Serialization.Attributes;
using SurveyApp.Domain.Attributes;
using SurveyApp.Domain.Common;
using SurveyApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities
{
    [BsonCollection("Users")]
    public class User : MongoEntity
    {
        [BsonElement("email")]
        public string Email { get;   set; }

        [BsonElement("passwordHash")]
        public string PasswordHash { get;   set; }

        [BsonElement("surveys")]
        private readonly List<Survey> _surveys = new();

        [BsonIgnore]
        public IReadOnlyCollection<Survey> Surveys => _surveys.AsReadOnly();

        protected User()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public static User Create(string email, string passwordHash)
        {
            if (string.IsNullOrEmpty(email))
                throw new DomainException("Email cannot be empty");

            return new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                CreatedAt = DateTime.UtcNow,
                PasswordHash=passwordHash
            };
        }

        public void UpdatePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
        }
    }
}

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyApp.Domain.Attributes;
using SurveyApp.Domain.Common;

namespace SurveyApp.Domain.Entities
{
    [BsonCollection("Votes")]
    public class Vote : MongoEntity
    {
        [BsonElement("userId")]
        [BsonRepresentation(BsonType.String)]
        public Guid UserId { get; set; }

        [BsonElement("optionId")]
        [BsonRepresentation(BsonType.String)]
        public Guid OptionId { get; set; }

        [BsonElement("surveyId")]
        [BsonRepresentation(BsonType.String)]
        public Guid SurveyId { get; set; }

        public Vote() { }

        public static Vote Create(Guid userId, Guid optionId, Guid surveyId)
        {
            return new Vote
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                OptionId = optionId,
                SurveyId = surveyId,
                CreatedAt = DateTime.UtcNow,
                CreatorId = userId
            };
        }
    }
}

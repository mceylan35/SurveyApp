using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SurveyApp.Domain.Attributes;
using SurveyApp.Domain.Common;
using SurveyApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities
{
    [BsonCollection("Options")]
    public class Option : MongoEntity
    {
        [BsonElement("text")]
        public string Text { get; set; }
        [BsonElement("surveyId")]
        [BsonRepresentation(BsonType.String)]
        public Guid SurveyId { get; private set; }

        [BsonElement("voteCount")]
        public int VoteCount { get; set; }

        public Option() { }

        public static Option Create(string text, Guid surveyId)
        {
            if (string.IsNullOrEmpty(text))
                throw new DomainException("Option text cannot be empty");

            return new Option
            {
                Id = Guid.NewGuid(),
                Text = text,
                SurveyId = surveyId,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void IncrementVoteCount()
        {
            VoteCount++;
        }
    }
}

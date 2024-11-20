using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities
{
    public class Option : MongoEntity
    {
        [BsonElement("text")]
        public string Text { get; private set; }

        [BsonElement("voteCount")]
        public int VoteCount { get; private set; }

        protected Option() { }

        public static Option Create(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new DomainException("Option text cannot be empty");

            return new Option
            {
                Id = Guid.NewGuid(),
                Text = text,
                VoteCount = 0,
                CreatedAt = DateTime.UtcNow
            };
        }

        internal void IncrementVoteCount()
        {
            VoteCount++;
        }
    }
}

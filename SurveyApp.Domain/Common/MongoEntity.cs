using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Common
{
    public abstract class MongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; protected set; }

        [BsonElement("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
        [BsonElement("creatorId")]
        [BsonRepresentation(BsonType.String)]
        public Guid CreatorId { get;  set; }
        protected MongoEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
